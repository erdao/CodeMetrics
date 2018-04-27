using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace ILCyclomicComplextityCalculator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectAssembly_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            var openFileResult = openFileDialog.ShowDialog();
            if (openFileResult is true)
            {
                AssemblyPath.Text = openFileDialog.FileName;
            }
        }

        private async void Resolve_OnClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CodeSmells.Text, out int codeSmells) && codeSmells > 1 && codeSmells < 100)
            {
                try
                {
                    ResultView.Items.Clear();
                    var assemblyResult =await AssemblyResolver.Resolver(AssemblyPath.Text, codeSmells);
                    foreach (var typeResult in assemblyResult.TypeResults)
                    {
                        var treeViewItem = new TreeViewItem() { Header = typeResult.TypeName };
                        int codeSmellCount = 0;
                        foreach (var methodResult in typeResult.MethodResults)
                        {
                            treeViewItem.Items.Add(
                                new ResultItem()
                                {
                                    MethodName = methodResult.MethodName,
                                    ILCC = methodResult.ILCc.ToString(),
                                    PassOrNot = methodResult.IsPass
                                });
                            if (!methodResult.IsPass)
                            {
                                codeSmellCount++;
                            }
                        }
                        
                        ChangeFace(treeViewItem, double.Parse(codeSmellCount.ToString()) / typeResult.MethodResults.Count);
                        ResultView.Items.Add(treeViewItem);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                }
            }

        }

        private void ChangeFace(TreeViewItem item, double percent)
        {
            if (percent > 0.1 && percent < 0.2)
            {
                item.Background = CreateBrush("#f9bdbb");
            }
            else if (percent > 0.2&& percent <0.3)
            {
                item.Background = CreateBrush("#f69988");
            }
            else if (percent > 0.3 && percent < 0.4)
            {
                item.Background = CreateBrush("#f36c60");
            }
            else if (percent > 0.4)
            {
                item.Background = CreateBrush("#e51c23");
            }
        }

        private Brush CreateBrush(string color)
        {
            // ReSharper disable once PossibleNullReferenceException
            return new SolidColorBrush((Color) ColorConverter.ConvertFromString(color));
        }
    }
}
