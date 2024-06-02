using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using ПР52_Осокин.Pages;

namespace ПР52_Осокин.Classes
{
    public class Report
    {
        public static void Group(int IdGroup, Main main)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            SaveFileDialog SFD = new SaveFileDialog()
            {
                InitialDirectory = path,
                Filter = "Excel file (*.xlsx)|*.xlsx",
            };
            SFD.ShowDialog();
            if (SFD.FileName != "")
            {
                GroupContext Group = main.AllGroups.Find(x => x.Id == IdGroup);
                var ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                int idBestStudent = 0;
                int temp = 1000;
                bool IsComplete = false;
                try
                {
                    ExcelApp.Visible = false;
                    Microsoft.Office.Interop.Excel.Workbook Workbook = ExcelApp.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel.Worksheet Worksheet = Workbook.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;
                    Worksheet.Name = Group.Name;
                    (Worksheet.Cells[1, 1] as Microsoft.Office.Interop.Excel.Range).Value = $"Отчёт о группе {Group.Name}";
                    Worksheet.Range[Worksheet.Cells[1, 1], Worksheet.Cells[1, 5]].Merge();
                    Styles((Range)Worksheet.Cells[1, 1], 18);
                    (Worksheet.Cells[3, 1] as Microsoft.Office.Interop.Excel.Range).Value = $"Список группы:";
                    Worksheet.Range[Worksheet.Cells[3, 1], Worksheet.Cells[3, 5]].Merge();
                    Styles((Range)Worksheet.Cells[3, 1], 12, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft);
                    (Worksheet.Cells[4, 1] as Microsoft.Office.Interop.Excel.Range).Value = $"ФИО";
                    Styles((Range)Worksheet.Cells[4, 1], 12, XlHAlign.xlHAlignCenter, true);
                    (Worksheet.Cells[4, 1] as Microsoft.Office.Interop.Excel.Range).ColumnWidth = 35.0f;
                    (Worksheet.Cells[4, 2] as Microsoft.Office.Interop.Excel.Range).Value = $"Кол-во не сданных практических";
                    Styles((Range)Worksheet.Cells[4, 2], 12, XlHAlign.xlHAlignCenter, true);
                    (Worksheet.Cells[4, 3] as Microsoft.Office.Interop.Excel.Range).Value = $"Кол-во не сданных теоретических";
                    Styles((Range)Worksheet.Cells[4, 3], 12, XlHAlign.xlHAlignCenter, true);
                    (Worksheet.Cells[4, 4] as Microsoft.Office.Interop.Excel.Range).Value = $"Отсутствовал на паре";
                    Styles((Range)Worksheet.Cells[4, 4], 12, XlHAlign.xlHAlignCenter, true);
                    (Worksheet.Cells[4, 5] as Microsoft.Office.Interop.Excel.Range).Value = $"Опоздал";
                    Styles((Range)Worksheet.Cells[4, 5], 12, XlHAlign.xlHAlignCenter, true);
                    int Height = 5;
                    List<StudentContext> Students = main.AllStudents.FindAll(x => x.IdGroup == IdGroup);
                    foreach (StudentContext student in Students)
                    {
                        List<DisciplineContext> StudentDisciplines = main.AllDisciplines.FindAll(x => x.IdGroup == student.IdGroup);
                        int PracticeCount = 0;
                        int TheoryCount = 0;
                        int AbsenteeismCount = 0;
                        int LateCount = 0;
                        foreach (DisciplineContext StudentDiscipline in StudentDisciplines)
                        {
                            List<WorkContext> StudentWorks = main.AllWorks.FindAll(x => x.IdDiscipline == StudentDiscipline.Id);
                            foreach (WorkContext StudentWork in StudentWorks)
                            {
                                EvaluationContext Evaluation = main.AllEvaluations.Find(x => x.IdWork == StudentWork.Id && x.IdStudent == student.Id);
                                if ((Evaluation != null && (Evaluation.Value.Trim() == "" || Evaluation.Value.Trim() == "2"))
                                    || Evaluation == null)
                                {
                                    if (StudentWork.IdType == 1)
                                        PracticeCount++;
                                    else if (StudentWork.IdType == 2)
                                        TheoryCount++;
                                }
                                if (Evaluation != null && Evaluation.Lateness.Trim() != "")
                                {
                                    if (Convert.ToInt32(Evaluation.Lateness) == 90)
                                        AbsenteeismCount++;
                                    else LateCount++;
                                }

                            }
                        }
                        if ((PracticeCount + TheoryCount + AbsenteeismCount + LateCount) < temp)
                        {
                            temp = PracticeCount + TheoryCount + AbsenteeismCount + LateCount;
                            idBestStudent = Height;
                        }
                        (Worksheet.Cells[Height, 1] as Microsoft.Office.Interop.Excel.Range).Value = $"{student.Lastname} {student.Firstname}";
                        Styles(Worksheet.Cells[Height, 1] as Microsoft.Office.Interop.Excel.Range, 12, XlHAlign.xlHAlignLeft, true);
                        (Worksheet.Cells[Height, 2] as Microsoft.Office.Interop.Excel.Range).Value = PracticeCount.ToString();
                        Styles(Worksheet.Cells[Height, 2] as Microsoft.Office.Interop.Excel.Range, 12, XlHAlign.xlHAlignCenter, true);
                        (Worksheet.Cells[Height, 3] as Microsoft.Office.Interop.Excel.Range).Value = TheoryCount.ToString();
                        Styles(Worksheet.Cells[Height, 3] as Microsoft.Office.Interop.Excel.Range, 12, XlHAlign.xlHAlignCenter, true);
                        (Worksheet.Cells[Height, 4] as Microsoft.Office.Interop.Excel.Range).Value = AbsenteeismCount.ToString();
                        Styles(Worksheet.Cells[Height, 4] as Microsoft.Office.Interop.Excel.Range, 12, XlHAlign.xlHAlignCenter, true);
                        (Worksheet.Cells[Height, 5] as Microsoft.Office.Interop.Excel.Range).Value = LateCount.ToString();
                        Styles(Worksheet.Cells[Height, 5] as Microsoft.Office.Interop.Excel.Range, 12, XlHAlign.xlHAlignCenter, true);
                        Height++;
                    }
                    Worksheet.Range[Worksheet.Cells[idBestStudent, 1], Worksheet.Cells[idBestStudent, 5]].Interior.Color = ColorTranslator.ToOle(System.Drawing.Color.Green);
                    Workbook.SaveAs(SFD.FileName);
                    IsComplete = true;
                    Workbook.Close();
                    if (IsComplete)
                    {
                        MessageBox.Show($"Успешный импорт файла '{SFD.FileName}'.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ExcelApp.Quit();
            }
        }
        public static void Styles(Microsoft.Office.Interop.Excel.Range Cell,
            int FontSize, Microsoft.Office.Interop.Excel.XlHAlign Position = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter,
            bool Border = false)
        {
            Cell.Font.Name = "Bahnschhrift Light Condensed";
            Cell.Font.Size = FontSize;
            Cell.HorizontalAlignment = Position;
            Cell.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            if (Border)
            {
                Microsoft.Office.Interop.Excel.Borders border = Cell.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble;
                border.Weight = XlBorderWeight.xlThin;
                Cell.WrapText = true;
            }
        }
    }
}
