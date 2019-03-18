using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Syncfusion.BarcodeReader.OPX;
using Syncfusion.Pdf.Parsing;

namespace BarcodeReaderSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Clear text box
            txtResult.Text = string.Empty;

            string[] words = txtImageFile.Text.Split('.');
            string extension = words[words.Length - 1];

            if (extension.ToLower() == "png" || extension.ToLower() == "bmp" || extension.ToLower() == "jpg")
            {
                if (txtImageFile.Text != "" && txtImageFile.Text != string.Empty)
                {
                    if (checkBox1.Checked)
                    {
                        //Read multiple barcodes
                        BarcodeResultCollection result = BarcodeReader.ScanMultipleBarcode(txtImageFile.Text);
                        AddText(result);
                    }
                    else
                    {
                        //Read barcode
                        BarcodeResult result = BarcodeReader.ScanBarcode(txtImageFile.Text);
                        AddText(result);
                    }
                }

            }
            else if (extension.ToLower() == "pdf")
            {
                Bitmap[] images;

                //Load existing document
                PdfLoadedDocument ldoc = new PdfLoadedDocument(txtImageFile.Text);

                //Export the document as images
                images = ldoc.ExportAsImage(0, ldoc.Pages.Count - 1);

                //Scan barcodes
                foreach (Bitmap img in images)
                {
                    BarcodeReader reader = new BarcodeReader(img);
                    if (checkBox1.Checked)
                    {
                        reader.Settings.TryHarder = true;
                        BarcodeResultCollection results = reader.ScanMultipleBarcode();
                        AddText(results);
                    }
                    else
                    {
                        BarcodeResult result = reader.ScanBarcode();
                        AddText(result);
                    }
                }
            }

            else
                MessageBox.Show("Please select a barcode image or PDF", "Barcode Reader", MessageBoxButtons.OK);

        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Images (*.BMP;*.JPG;*.PNG;*.PDF)|*.BMP;*.JPG;*.PNG;*.PDF";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtImageFile.Text = openFileDialog1.FileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtImageFile.Text = Path.GetFullPath("../../Data/Barcode.pdf");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtResult.Text = string.Empty;

            if (txtImageFile.Text != "")
            {
                string[] words = txtImageFile.Text.Split('.');
                string extension = words[words.Length - 1];

                if (extension.ToLower() == "pdf")
                {
                    //Create a new barcode reader
                    BarcodeReader reader = new BarcodeReader(txtImageFile.Text, FormatType.PDF);

                    if (this.checkBox1.Checked)
                    {
                        //Scan multiple barcodes
                        BarcodeResultCollection results = reader.ScanMultipleBarcode();
                        DisplayResult(results);
                    }
                    else
                    {
                        //scan barcode
                        BarcodeResult result = reader.ScanBarcode();
                        DisplayResult(result);
                    }
                }
                else
                    MessageBox.Show("Please select PDF", "Barcode Reader", MessageBoxButtons.OK);
            }
        }

        private void AddText(BarcodeResult result)
        {
            if (result != null)
            {
                txtResult.Text += "Barcode Type: " + result.BarcodeType + " " + "Text: " + result.Text + "\r\n";
            }
            else
                MessageBox.Show("Barcode could not be detected", "Barcode Reader", MessageBoxButtons.OK);
        }

        private void AddText(BarcodeResultCollection results)
        {
            if (results != null && results.Count > 0)
                for (int i = results.Count - 1; i >= 0; i--)
                    txtResult.Text += "Barcode Type: " + results[i].BarcodeType + " " + "Text: " + results[i].Text + "\r\n";
            else
                MessageBox.Show("Barcode could not be detected", "Barcode Reader", MessageBoxButtons.OK);
        }

        private void DisplayResult(BarcodeResult result)
        {
            if (result != null)
                txtResult.Text += "Barcode Type: " + result.BarcodeType + " " + "Text: " + result.Text + "\r\n";
            else
                MessageBox.Show("Barcode could not be detected", "Barcode Reader", MessageBoxButtons.OK);
        }

        private void DisplayResult(BarcodeResultCollection results)
        {
            if (results != null && results.Count > 0)
                for (int i = 0; i < results.Count; i++)
                    txtResult.Text += "Barcode Type: " + results[i].BarcodeType + " " + "Text: " + results[i].Text + "\r\n";
            else
                MessageBox.Show("Barcode could not be detected", "Barcode Reader", MessageBoxButtons.OK);
        }
    }
}
