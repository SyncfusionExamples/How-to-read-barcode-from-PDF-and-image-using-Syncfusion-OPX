# How to read barcode from PDF and image using Syncfusion&reg; OPX

ZXing (zebra crossing) is an open-source tool to decode barcodes within images that fall under the Apache 2.0 license. It allows users to scan most of the 1D and 2D barcodes, including QR codes and data matrix barcodes.

Syncfusion&reg; [Barcode Reader OPX](https://www.syncfusion.com/products/opx/barcode) is used to optimize the working of ZXing with Syncfusion&reg; [.NET PDF library](https://www.syncfusion.com/pdf-framework/net). This scans the barcode from the PDF document and returns the barcode value and type.

## Assembly Requirement
To use the Barcode Reader feature in applications, references need to be added to the following set of assemblies:

Assembly Name | Description
--- | ---
Syncfusion.Pdf.Base | This assembly contains the core feature for manipulating and saving PDF documents.
Syncfusion.Compression.Base | This assembly compresses the internal contents of a PDF document.
Syncfusion.BarcodeReader.OPX | This assembly is the wrapper for the ZXing assembly using ZXing features.
zxing | Decodes barcode within the image.

The following namespace should be included in the application:

```C#
using Syncfusion.BarcodeReader.OPX;
using Syncfusion.Pdf.Parsing;
```
## Scanning Embedded PDF barcode images:

If the PDF contains barcodes as shapes, then it internally converts the PDF page into an image and then detects the barcode. The following is the code snippet for this:

```C#
BarcodeReader reader = new BarcodeReader("Barcode.pdf";, FormatType.PDF);
BarcodeResult result = reader.ScanBarcode();
```

## Scanning PDF barcodes:

If the PDF contains a barcode as an image, then the image alone can be extracted and the barcode will then be detected. The following is the code snippet for this:

```C#
//Loads the existing document.
PdfLoadedDocument document = new PdfLoadedDocument(txtImageFile.Text);

//Exports the document as images.
images = document.ExportAsImage(0, document.Pages.Count);
foreach (Bitmap img in images)
{
	BarcodeReader reader = new BarcodeReader(img);
	BarcodeResult result = reader.ScanBarcode();
}
```

<b>Supported Barcode types are:</b> Aztec, Codabar, Code 39, Code 93, Code 128, Data Martix, EAN-8, EAN-13, IMB, ITF, MaxiCode, PDF417, QR Code, UPC-A, UPC-E, MSI.
