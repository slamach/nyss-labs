using ParserApp.Models;
using ParserApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ParserApp.Views
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public const string FILE_ADDRESS = "https://bdu.fstec.ru/files/documents/thrlist.xlsx";
        public const string DATA_PATH = "thrlist.xlsx";

        private int _currentPage;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        public List<int> ItemsPerPageList { get; set; } = new List<int> { 15, 30, 50 };
        public int ItemsPerPage { get; set; }

        private List<SecurityThreat> allList;
        private BindingList<SecurityThreat> currentList = new BindingList<SecurityThreat>();

        public BindingList<UpdateResult> UpdateResultList { get; set; } = new BindingList<UpdateResult>();
        private int _threatsAdded;
        public int ThreatsAdded
        {
            get { return _threatsAdded; }
            set
            {
                _threatsAdded = value;
                OnPropertyChanged("ThreatsAdded");
            }
        }
        private int _threatsUpdated;
        public int ThreatsUpdated
        {
            get { return _threatsUpdated; }
            set
            {
                _threatsUpdated = value;
                OnPropertyChanged("ThreatsUpdated");
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            ItemsPerPage = ItemsPerPageList[0];
        }

        private void RefreshCurrentList()
        {
            if (allList == null || allList.Count == 0)
            {
                return;
            }
            currentList.Clear();

            for (int i = ItemsPerPage * (CurrentPage - 1); i < ItemsPerPage * CurrentPage; i++)
            {
                if (i >= allList.Count)
                {
                    break;
                }
                currentList.Add(allList[i]);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bool isDownloaded = true;

            if (!File.Exists(DATA_PATH))
            {
                // TODO: Нормальное оповещение пользователя
                MessageBox.Show("Сейчас пойдет первичная загрузка данных...");
                if (!DownloadService.DownloadFile(FILE_ADDRESS, DATA_PATH))
                {
                    MessageBox.Show(
                        "Проверьте подключение и нажмите 'Обновить' для повторной попытки.",
                        "При загрузке данных произошла ошибка!");
                    isDownloaded = false;
                }
            }

            if (isDownloaded)
            {
                if (!ExcelService.GetListFromFile(DATA_PATH, out allList))
                {
                    MessageBox.Show(
                        "Попробуйте вручную удалить файл с данными и повторить попытку.",
                        "При чтении данных произошла ошибка!");
                }
            }

            RefreshCurrentList();

            dgThreatsList.ItemsSource = currentList;
            lvUpdateResults.ItemsSource = UpdateResultList;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!DownloadService.DownloadFile(FILE_ADDRESS, DATA_PATH))
            {
                MessageBox.Show(
                    "Проверьте подключение и попробуйте еще раз.",
                    "При загрузке данных произошла ошибка!");
            }
            else
            {
                List<SecurityThreat> updatedList;
                if (!ExcelService.GetListFromFile(DATA_PATH, out updatedList))
                {
                    MessageBox.Show(
                        "Попробуйте вручную удалить файл с данными и повторить попытку.",
                        "При чтении данных произошла ошибка!");
                }
                else if (updatedList != null)
                {
                    ThreatsAdded = 0;
                    ThreatsUpdated = 0;
                    UpdateResultList.Clear();

                    bool threatUpdated;
                    bool showReport = true;

                    for (int i = 0; i < updatedList.Count; i++)
                    {
                        if (i >= allList.Count)
                        {
                            ThreatsAdded++;
                            continue;
                        }

                        threatUpdated = false;

                        if (updatedList.Count < allList.Count || updatedList[i].Id != allList[i].Id)
                        {
                            MessageBox.Show(
                                "Отчёт не будет сформирован, все изменения отобразятся в таблице.",
                                "Порядок следования угроз был изменен!");
                            showReport = false;
                            break;
                        }
                        if (updatedList[i].Name != allList[i].Name)
                        {
                            UpdateResultList.Add(new UpdateResult(
                                updatedList[i].Id,
                                "Наименование угрозы",
                                allList[i].Name,
                                updatedList[i].Name));
                            threatUpdated = true;
                        }
                        if (updatedList[i].Description != allList[i].Description)
                        {
                            UpdateResultList.Add(new UpdateResult(
                                updatedList[i].Id,
                                "Описание угрозы",
                                allList[i].Description,
                                updatedList[i].Description));
                            threatUpdated = true;
                        }
                        if (updatedList[i].Source != allList[i].Source)
                        {
                            UpdateResultList.Add(new UpdateResult(
                                updatedList[i].Id,
                                "Источник угрозы",
                                allList[i].Source,
                                updatedList[i].Source));
                            threatUpdated = true;
                        }
                        if (updatedList[i].Target != allList[i].Target)
                        {
                            UpdateResultList.Add(new UpdateResult(
                                updatedList[i].Id,
                                "Объект взаимодействия",
                                allList[i].Target,
                                updatedList[i].Target));
                            threatUpdated = true;
                        }
                        if (updatedList[i].ConfViolated != allList[i].ConfViolated)
                        {
                            UpdateResultList.Add(new UpdateResult(
                                updatedList[i].Id,
                                "Нарушение конфидециальности",
                                allList[i].ConfViolated,
                                updatedList[i].ConfViolated));
                            threatUpdated = true;
                        }
                        if (updatedList[i].IntegViolated != allList[i].IntegViolated)
                        {
                            UpdateResultList.Add(new UpdateResult(
                                updatedList[i].Id,
                                "Нарушение целостности",
                                allList[i].IntegViolated,
                                updatedList[i].IntegViolated));
                            threatUpdated = true;
                        }
                        if (updatedList[i].AccessViolated != allList[i].AccessViolated)
                        {
                            UpdateResultList.Add(new UpdateResult(
                                updatedList[i].Id,
                                "Нарушение доступности",
                                allList[i].AccessViolated,
                                updatedList[i].AccessViolated));
                            threatUpdated = true;
                        }

                        if (threatUpdated)
                        {
                            ThreatsUpdated++;
                        }
                    }

                    if (showReport)
                    {
                        popUpdate.IsOpen = true;
                    }

                    allList.Clear();
                    foreach (SecurityThreat threat in updatedList)
                    {
                        allList.Add(threat);
                    }
                    CurrentPage = 1;
                    RefreshCurrentList();
                }
            }
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                RefreshCurrentList();
            }
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (
                (allList.Count % ItemsPerPage == 0) && (CurrentPage < allList.Count / ItemsPerPage) ||
                (allList.Count % ItemsPerPage != 0) && (CurrentPage <= allList.Count / ItemsPerPage))
            {
                CurrentPage++;
                RefreshCurrentList();
            }
        }

        private void cboItemsPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentPage = 1;
            RefreshCurrentList();
        }

        private void dgThreatsList_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = sender as DataGrid;

            if (grid != null)
            {
                FrameworkElement element = e.OriginalSource as FrameworkElement;

                if (element?.DataContext is SecurityThreat)
                {
                    if (grid.SelectedItem == (SecurityThreat)((FrameworkElement)e.OriginalSource).DataContext)
                    {
                        grid.SelectedIndex = -1;
                        e.Handled = true;
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
