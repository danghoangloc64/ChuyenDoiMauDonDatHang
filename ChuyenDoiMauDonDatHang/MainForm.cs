using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using ExcelDataReader;
using iTextSharp.text;
using iTextSharp.text.pdf;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChuyenDoiMauDonDatHang
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        List<string> m_listPDFPath;
        private List<DataOrderReportModel> m_listDataOrderReportModels;
        private List<DataOrderModel> m_listDataOrderModels;
        private List<string> m_listMaVanDonCoKeoNgot;
        public MainForm()
        {
            InitializeComponent();
        }

        private int GetColumnNumber(string name)
        {
            name = name.ToUpper();
            int number = 0;
            int pow = 1;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                pow *= 26;
            }

            return number;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Check.CheckTime();
            txtFolderOutput.Text = Properties.Settings.Default.OutputPath;
            txtPDFInput.Text = Properties.Settings.Default.PDFInput;
        }

        private void btnSelectOutputPath_Click(object sender, EventArgs e)
        {
            Check.CheckTime();
            using (var folderSelect = new FolderBrowserDialog())
            {
                if (folderSelect.ShowDialog() == DialogResult.OK)
                {
                    txtFolderOutput.Text = folderSelect.SelectedPath;
                    Properties.Settings.Default.OutputPath = folderSelect.SelectedPath;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void btnSelectExcel_Click(object sender, EventArgs e)
        {
            Check.CheckTime();
            using (var fileSelect = new OpenFileDialog())
            {
                fileSelect.Multiselect = false;
                if (fileSelect.ShowDialog() == DialogResult.OK)
                {
                    txtExcelPath.Text = fileSelect.FileName;
                }
            }
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPDFInput.Text) || string.IsNullOrEmpty(txtExcelPath.Text) || string.IsNullOrEmpty(txtFolderOutput.Text))
            {
                XtraMessageBox.Show(this, "Vui long nhap day du thong tin.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            m_listPDFPath = Directory.GetFiles(txtPDFInput.Text, "*.pdf", SearchOption.AllDirectories).ToList();
            if (m_listPDFPath.Count == 0)
            {
                richTextBoxLog.Invoke(new Action(() =>
                {
                    richTextBoxLog.Clear();
                }));

                richTextBoxLog.Invoke(new Action(() =>
                {
                    richTextBoxLog.Text += "Khong co file pdf trong folder" + Environment.NewLine;
                }));
                return;
            }

            Check.CheckTime();
            try
            {
                string filePath = txtExcelPath.Text;
                string stringTextFind = txtFindText.Text.ToLower();
                m_listDataOrderModels = new List<DataOrderModel>();
                m_listMaVanDonCoKeoNgot = new List<string>();
                m_listDataOrderReportModels = new List<DataOrderReportModel>();
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                    {
                        DataSet result = reader.AsDataSet();
                        DataTable dt = result.Tables[0];
                        for (int i = 1; i < dt.Rows.Count; i++)
                        {
                            DataOrderModel dataOrderModel = new DataOrderModel()
                            {
                                MaVanDon = dt.Rows[i][GetColumnNumber("F") - 1].ToString(),
                                SanPham = dt.Rows[i][GetColumnNumber("O") - 1].ToString(),
                                PhanLoaiHang = dt.Rows[i][GetColumnNumber("T") - 1].ToString(),
                                NgayDatHang = dt.Rows[i][GetColumnNumber("C") - 1].ToString().Substring(0, 10),
                                TongTien = dt.Rows[i][GetColumnNumber("AO") - 1].ToString(),
                                MaDonHang = dt.Rows[i][GetColumnNumber("A") - 1].ToString(),
                                SoLuong = dt.Rows[i][GetColumnNumber("Z") - 1].ToString(),
                                NguoiNhan = dt.Rows[i][GetColumnNumber("BC") - 1].ToString(),
                                DonViVanChuyen = dt.Rows[i][GetColumnNumber("G") - 1].ToString(),
                            };
                            m_listDataOrderModels.Add(dataOrderModel);
                        }
                    }
                }

                var checkExistsKeoNgot = m_listDataOrderModels.Where(x => x.PhanLoaiHang.ToLower().Contains(stringTextFind));

                if (checkExistsKeoNgot.Count() == 0)
                {
                    XtraMessageBox.Show(this, $"Không có mã vận đơn nào có khuyến mãi {stringTextFind}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int countMaVanDonKeoNgot = checkExistsKeoNgot.Select(x => x.MaVanDon).Distinct().Count();
                    m_listMaVanDonCoKeoNgot = new List<string>(checkExistsKeoNgot.Select(x => x.MaVanDon).Distinct());

                    await Task.Run(() =>
                    {
                        foreach (var maVanDonCoKetNgot in m_listMaVanDonCoKeoNgot)
                        {
                            List<SanPhamModel> dataSanPhams = new List<SanPhamModel>();
                            var datas = m_listDataOrderModels.Where(x => x.MaVanDon == maVanDonCoKetNgot && x.PhanLoaiHang.ToLower().Contains(stringTextFind) == false);
                            int iNoSanPham = 1;
                            foreach (var data in datas)
                            {
                                SanPhamModel sanPhamModel = new SanPhamModel()
                                {
                                    SanPham = $"{iNoSanPham++}. {data.SanPham} | {data.PhanLoaiHang} | SL: {data.SoLuong}"
                                };
                                dataSanPhams.Add(sanPhamModel);
                            }
                            SanPhamModel sanPhamModelQuaTang = new SanPhamModel()
                            {
                                SanPham = $"{iNoSanPham++}. Quà tặng viên kẹo | SL: nhiều"
                            };
                            dataSanPhams.Add(sanPhamModelQuaTang);

                            //QRCodeGenerator qrGenerator = new QRCodeGenerator();
                            //QRCodeData qrCodeData = qrGenerator.CreateQrCode(maVanDonCoKetNgot, QRCodeGenerator.ECCLevel.Q);
                            //QRCode qrCode = new QRCode(qrCodeData);
                            //Bitmap qrCodeImage = qrCode.GetGraphic(20);

                            //BarcodeLib.Barcode b = new BarcodeLib.Barcode(maVanDonCoKetNgot);
                            ////Image barCode = b.EncodedImage;


                            //CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"

                            //string a = double.Parse(datas.First().TongTien).ToString("#,###", cul.NumberFormat);


                            DataOrderReportModel dataOrderReportModel = new DataOrderReportModel()
                            {
                                ListSanPham = new List<SanPhamModel>(dataSanPhams),
                                //     ImageQRMaVanDon = qrCodeImage,
                                NgayDatHang = datas.First().NgayDatHang,
                                MaVanDon = "Mã vận đơn: " + datas.First().MaVanDon,
                                //   TongTien = a + " VND",
                                MaDonHang = "Mã đơn hàng: " + datas.First().MaDonHang,
                                //  ImageBarCodeMaVanDon = barCode,
                                NguoiNhan = datas.First().NguoiNhan,
                                DonViVanChuyen = datas.First().DonViVanChuyen,
                            };
                            m_listDataOrderReportModels.Add(dataOrderReportModel);
                        }

                        richTextBoxLog.Invoke(new Action(() =>
                        {
                            richTextBoxLog.Clear();
                        }));

                        if (Directory.Exists("temp"))
                        {
                            Directory.Delete("temp", true);
                        }
                        Directory.CreateDirectory("temp");
                        foreach (var report in m_listDataOrderReportModels)
                        {
                            richTextBoxLog.Invoke(new Action(() =>
                            {
                                richTextBoxLog.Text += "Đang xử lý mã vận đơn " + report.MaVanDon + Environment.NewLine;
                            }));


                            var checkPDF = m_listPDFPath.FirstOrDefault(x => Path.GetFileNameWithoutExtension(x).ToLower() == report.MaVanDon.Replace("Mã vận đơn: ", "").ToLower());
                            if (checkPDF == null)
                            {
                                richTextBoxLog.Invoke(new Action(() =>
                                {
                                    richTextBoxLog.Text += "***Mã vận đơn " + report.MaVanDon + " không tồn tại" + Environment.NewLine;
                                }));
                                continue;
                            }




                            using (Stream inputPdfStream = new FileStream(checkPDF, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                using (Stream outputPdfStream = new FileStream(Path.Combine("temp", Path.GetFileName(checkPDF)), FileMode.Create, FileAccess.Write, FileShare.None))
                                {
                                    var reader = new PdfReader(inputPdfStream);
                                    var stamper = new PdfStamper(reader, outputPdfStream);
                                    var pdfContentByte = stamper.GetOverContent(1);

                                    if (report.DonViVanChuyen.Contains("Xpress"))
                                    {
                                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance("white-shopee-xpress.png");
                                        image.SetAbsolutePosition(11, 127);
                                        pdfContentByte.AddImage(image);
                                    }
                                    else
                                    {
                                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance("white-giao-hang-nhanh.png");
                                        image.SetAbsolutePosition(9, 103);
                                        pdfContentByte.AddImage(image);
                                    }

                                    stamper.Close();
                                }
                            }

                            string strAdd = string.Empty;
                            foreach (var sp in report.ListSanPham)
                            {
                                string sanPham = sp.SanPham;
                                int spaceIndex = 10;
                                int colonPosition = CustomIndexOf(sanPham, ' ', spaceIndex++);
                                if (colonPosition == -1)
                                {
                                    strAdd += sanPham;
                                }
                                else
                                {
                                    while (colonPosition < 60)
                                    {
                                        colonPosition = CustomIndexOf(sanPham, ' ', spaceIndex++);
                                        if (colonPosition == -1)
                                        {
                                            //spaceIndex = spaceIndex - 2;
                                            //colonPosition = CustomIndexOf(sanPham, ' ', spaceIndex);
                                            break;
                                        }
                                    }
                                    if (colonPosition == -1)
                                    {
                                       // strAdd += sp.SanPham;
                                    }
                                    else
                                    {
                                        sanPham = sanPham.Insert(colonPosition, Environment.NewLine);
                                        spaceIndex += 10;
                                        colonPosition = CustomIndexOf(sanPham, ' ', spaceIndex++);
                                        if (colonPosition == -1)
                                        {
                                            //strAdd += sp.SanPham;
                                        }
                                        else
                                        {
                                            while (colonPosition < 125)
                                            {
                                                colonPosition = CustomIndexOf(sanPham, ' ', spaceIndex++);
                                                if (colonPosition == -1)
                                                {
                                                    //spaceIndex = spaceIndex - 2;
                                                    //colonPosition = CustomIndexOf(sanPham, ' ', spaceIndex);
                                                    break;
                                                }
                                            }
                                            if (colonPosition == -1)
                                            {
                                               // strAdd += sp.SanPham;
                                            }
                                            else
                                            {
                                                sanPham = sanPham.Insert(colonPosition, Environment.NewLine);
                                                spaceIndex += 10;
                                                colonPosition = CustomIndexOf(sanPham, ' ', spaceIndex++);
                                                if (colonPosition == -1)
                                                {
                                                    //strAdd += sp.SanPham;
                                                }
                                                else
                                                {
                                                    while (colonPosition < 185)
                                                    {
                                                        colonPosition = CustomIndexOf(sanPham, ' ', spaceIndex++);
                                                        if (colonPosition == -1)
                                                        {
                                                            spaceIndex = spaceIndex - 2;
                                                            colonPosition = CustomIndexOf(sanPham, ' ', spaceIndex);
                                                            break;
                                                        }
                                                    }
                                                    sanPham = sanPham.Insert(colonPosition, Environment.NewLine);
                                                    spaceIndex += 10;
                                                    colonPosition = CustomIndexOf(sanPham, ' ', spaceIndex++);
                                                    if (colonPosition == -1)
                                                    {
                                                        //strAdd += sp.SanPham;
                                                    }
                                                    else
                                                    {
                                                        while (colonPosition < 250)
                                                        {
                                                            colonPosition = CustomIndexOf(sanPham, ' ', spaceIndex++);
                                                            if (colonPosition == -1)
                                                            {
                                                                //spaceIndex = spaceIndex - 2;
                                                                //colonPosition = CustomIndexOf(sanPham, ' ', spaceIndex);
                                                                break;
                                                            }
                                                        }
                                                        if (colonPosition == -1)
                                                        {
                                                            //strAdd += sp.SanPham;
                                                        }
                                                        else
                                                        {
                                                            sanPham = sanPham.Insert(colonPosition, Environment.NewLine);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    strAdd += sanPham;
                                }
                                strAdd += Environment.NewLine;
                            }


                            string pathOut = Path.Combine(txtFolderOutput.Text, report.MaVanDon.Replace("Mã vận đơn: ", "")) + ".pdf";
                            if (File.Exists(pathOut))
                            {
                                File.Delete(pathOut);
                            }

                            if (report.DonViVanChuyen.Contains("Xpress"))
                            {
                                AddTextToPdf(Path.Combine("temp", Path.GetFileName(checkPDF)),
                                pathOut,
                                strAdd, new Point(10, 178));
                            }
                            else
                            {
                                AddTextToPdf(Path.Combine("temp", Path.GetFileName(checkPDF)),
                                                               pathOut,
                                                               strAdd, new Point(10, 158));
                            }

                            //string pathOut = Path.Combine(txtFolderOutput.Text, report.MaVanDon.Replace("Mã vận đơn: ", "")) + ".pdf";
                            //if (File.Exists(pathOut))
                            //{
                            //    File.Delete(pathOut);
                            //}

                            //XtraReportOrder xtraReportOrder = new XtraReportOrder(report);
                            //xtraReportOrder.CreateDocument();
                            //xtraReportOrder.ExportToPdf(pathOut);
                        }
                    });

                    XtraMessageBox.Show(this, $"Có {countMaVanDonKeoNgot} mã vận đơn có {stringTextFind}.\r\nVui lòng mở folder để kiểm tra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);



                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectPDFInput_Click(object sender, EventArgs e)
        {
            Check.CheckTime();
            using (var folderSelect = new FolderBrowserDialog())
            {
                if (folderSelect.ShowDialog() == DialogResult.OK)
                {
                    txtPDFInput.Text = folderSelect.SelectedPath;
                    Properties.Settings.Default.PDFInput = folderSelect.SelectedPath;
                    Properties.Settings.Default.Save();



                }
            }
        }


        public void AddTextToPdf(string inputPdfPath, string outputPdfPath, string textToAdd, System.Drawing.Point point)
        {
            //variables
            string pathin = inputPdfPath;
            string pathout = outputPdfPath;

            //create PdfReader object to read from the existing document
            using (PdfReader reader = new PdfReader(pathin))
            //create PdfStamper object to write to get the pages from reader 
            using (PdfStamper stamper = new PdfStamper(reader, new FileStream(pathout, FileMode.Create)))
            {
                //select two pages from the original document
                reader.SelectPages("1-2");

                //gettins the page size in order to substract from the iTextSharp coordinates
                var pageSize = reader.GetPageSize(1);

                // PdfContentByte from stamper to add content to the pages over the original content
                PdfContentByte pbover = stamper.GetOverContent(1);

                string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");

                //Create a base font object making sure to specify IDENTITY-H
                BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);



                //add content to the page using ColumnText
                iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 6, iTextSharp.text.Font.NORMAL);

                //setting up the X and Y coordinates of the document
                int x = point.X;
                int y = point.Y;

                y = (int)(pageSize.Height - y);


                string[] lines = textToAdd.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                int nextY = 0;
                foreach (var line in lines)
                {
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(line, font), x, y - nextY, 0);
                    nextY += 10;
                }
            }
        }

        public int CustomIndexOf(string source, char toFind, int position)
        {
            int index = -1;
            for (int i = 0; i < position; i++)
            {
                index = source.IndexOf(toFind, index + 1);

                if (index == -1)
                    break;
            }

            return index;
        }
    }
}