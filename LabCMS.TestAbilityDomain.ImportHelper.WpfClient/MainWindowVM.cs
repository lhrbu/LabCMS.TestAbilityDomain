using LabCMS.TestAbilityDomain.ImportHelper.Shared.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LabCMS.TestAbilityDomain.ImportHelper.WpfClient
{
    public class MainWindowVM:BindableBase
    {
        private readonly XlsxParseService _xlsxParseService;
        public MainWindowVM(XlsxParseService xlsxParseService)
        { 
            _xlsxParseService = xlsxParseService;
            ParseCommand = new(async () =>
                {
                    try
                    {
                        NotInParsing = false;
                        Application.Current.MainWindow.Cursor = Cursors.Wait;
                        await _xlsxParseService.ParseImportXlsxAsync(ImportXlsx, OutputJson);
                        MessageBox.Show("Parse Successfully!", "Information");
                    }catch(Exception e) { MessageBox.Show(e.ToString()); }
                    finally
                    {
                        Application.Current.MainWindow.Cursor = Cursors.Arrow;
                        NotInParsing = true;
                    }
                });
            ResetCommand = new(() =>
            {
                ImportXlsx = Path.Combine(Environment.CurrentDirectory, "Import.xlsx");
                OutputJson = Path.Combine(Environment.CurrentDirectory, "Output.json");
            });
        }
        public string ImportXlsx { get => _importXlsx; set => SetProperty(ref _importXlsx, value); }
        private string _importXlsx = Path.Combine(Environment.CurrentDirectory,"Import.xlsx");
        public string OutputJson { get => _outputJson; set => SetProperty(ref _outputJson, value); }
        private string _outputJson = Path.Combine(Environment.CurrentDirectory, "Output.json");
        public bool NotInParsing { get => _notInParsing; set => SetProperty(ref _notInParsing, value); }
        private bool _notInParsing = true;
        public DelegateCommand ParseCommand { get; }
        public DelegateCommand ResetCommand { get; }
    }
}
