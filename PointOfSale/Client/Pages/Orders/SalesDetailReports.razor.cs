using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Net.ConnectCode.Barcode;
using PointOfSale.DAL.Domains;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Client.Pages.Orders
{
    public partial class SalesDetailReports : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        public int? id { get; set; }
        string barcode = "";
        string barcode_text = "";
        void GenerateBarcode()
        {
            BarcodeFonts bcf = new BarcodeFonts();
            bcf.BarcodeType = BarcodeFonts.BarcodeEnum.Code39;
            bcf.CheckDigit = BarcodeFonts.YesNoEnum.Yes;
            bcf.Data = "1234567";
            bcf.encode();
            barcode = bcf.EncodedData;
            barcode_text = bcf.HumanText;
        }
    }
}
