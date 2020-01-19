// ======================================================================
// 
//           filename : ExcelExporter_Tests.cs
//           description :
// 
//           created by ѩ�� at  2019-09-11 13:51
//           �ĵ�������https://docs.xin-lai.com
//           ���ںŽ̳̣�����ļ���
//           QQȺ��85318032����̽�����
//           Blog��http://www.cnblogs.com/codelove/
// 
// ======================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Tests.Models.Export;
using OfficeOpenXml;
using Shouldly;
using Xunit;
using Magicodes.ExporterAndImporter.Core.Extension;

namespace Magicodes.ExporterAndImporter.Tests
{
    public class ExcelExporter_Tests : TestBase
    {
        /// <summary>
        ///     ��entitiesֱ��ת��DataTable
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entities">entity����</param>
        /// <returns>��Entity��ֵתΪDataTable</returns>
        private static DataTable EntityToDataTable<T>(DataTable dt, IEnumerable<T> entities)
        {
            if (entities.Count() == 0) return dt;

            var properties = typeof(T).GetProperties();

            foreach (var entity in entities)
            {
                var dr = dt.NewRow();

                foreach (var property in properties)
                    if (dt.Columns.Contains(property.Name))
                        dr[property.Name] = property.GetValue(entity, null);

                dt.Rows.Add(dr);
            }

            return dt;
        }

        [Fact(DisplayName = "DTO���Ե��������Ը�ʽ����")]
        public async Task AttrsExport_Test()
        {
            IExporter exporter = new ExcelExporter();

            var filePath = GetTestFilePath($"{nameof(AttrsExport_Test)}.xlsx");

            DeleteFile(filePath);

            var data = GenFu.GenFu.ListOf<ExportTestDataWithAttrs>(100);
            foreach (var item in data)
            {
                item.LongNo = 45875266524;
            }
            var result = await exporter.Export(filePath, data);

            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
            using (var pck = new ExcelPackage(new FileInfo(filePath)))
            {
                pck.Workbook.Worksheets.Count.ShouldBe(1);
                var sheet = pck.Workbook.Worksheets.First();
                sheet.Cells[sheet.Dimension.Address].Rows.ShouldBe(101);

                //[ExporterHeader(DisplayName = "����1", Format = "yyyy-MM-dd")]
                sheet.Cells["E2"].Text.Equals(DateTime.Parse(sheet.Cells["E2"].Text).ToString("yyyy-MM-dd"));

                //[ExporterHeader(DisplayName = "����2", Format = "yyyy-MM-dd HH:mm:ss")]
                sheet.Cells["F2"].Text.Equals(DateTime.Parse(sheet.Cells["F2"].Text).ToString("yyyy-MM-dd HH:mm:ss"));

                //Ĭ��DateTime
                sheet.Cells["G2"].Text.Equals(DateTime.Parse(sheet.Cells["G2"].Text).ToString("yyyy-MM-dd"));

            }
        }

        [Fact(DisplayName = "�����ݵ���")]
        public async Task AttrsExportWithNoData_Test()
        {
            IExporter exporter = new ExcelExporter();

            var filePath = GetTestFilePath($"{nameof(AttrsExportWithNoData_Test)}.xlsx");

            DeleteFile(filePath);

            var data = new List<ExportTestDataWithAttrs>();
            var result = await exporter.Export(filePath, data);

            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
            using (var pck = new ExcelPackage(new FileInfo(filePath)))
            {
                pck.Workbook.Worksheets.Count.ShouldBe(1);
                pck.Workbook.Worksheets.First().Cells[pck.Workbook.Worksheets.First().Dimension.Address].Rows.ShouldBe(1);
            }
        }

        [Fact(DisplayName = "���ݲ�ֶ�Sheet����")]
        public async Task SplitData_Test()
        {
            IExporter exporter = new ExcelExporter();

            var filePath = GetTestFilePath($"{nameof(SplitData_Test)}-1.xlsx");

            DeleteFile(filePath);

            var result = await exporter.Export(filePath,
                GenFu.GenFu.ListOf<ExportTestDataWithSplitSheet>(300));

            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
            using (var pck = new ExcelPackage(new FileInfo(filePath)))
            {
                //��֤Sheet���Ƿ�Ϊ3
                pck.Workbook.Worksheets.Count.ShouldBe(3);
                //��������
                pck.Workbook.Worksheets.First().Cells["C1"].Value.ShouldBe("��ֵ");
                pck.Workbook.Worksheets.First().Cells[pck.Workbook.Worksheets.First().Dimension.Address].Rows.ShouldBe(101);
            }

            filePath = GetTestFilePath($"{nameof(SplitData_Test)}-2.xlsx");
            DeleteFile(filePath);

            result = await exporter.Export(filePath,
                GenFu.GenFu.ListOf<ExportTestDataWithSplitSheet>(299));

            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
            using (var pck = new ExcelPackage(new FileInfo(filePath)))
            {
                //��֤Sheet���Ƿ�Ϊ3
                pck.Workbook.Worksheets.Count.ShouldBe(3);
                //�벻Ҫʹ��������NET461��.NET Core��Sheet����ֵ��һ�£�
                var lastSheet = pck.Workbook.Worksheets.Last();
                lastSheet.Cells[lastSheet.Dimension.Address].Rows.ShouldBe(100);
            }

            filePath = GetTestFilePath($"{nameof(SplitData_Test)}-3.xlsx");
            DeleteFile(filePath);

            result = await exporter.Export(filePath,
                GenFu.GenFu.ListOf<ExportTestDataWithSplitSheet>(302));

            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
            using (var pck = new ExcelPackage(new FileInfo(filePath)))
            {
                //��֤Sheet���Ƿ�Ϊ4
                pck.Workbook.Worksheets.Count.ShouldBe(4);
                //�벻Ҫʹ��������NET461��.NET Core��Sheet����ֵ��һ�£�
                var lastSheet = pck.Workbook.Worksheets.Last();
                lastSheet.Cells[lastSheet.Dimension.Address].Rows.ShouldBe(3);
            }
        }

        [Fact(DisplayName = "ͷ��ɸѡ������")]
        public async Task ExporterHeaderFilter_Test()
        {
            IExporter exporter = new ExcelExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{nameof(ExporterHeaderFilter_Test)}.xlsx");
            #region ͨ��ɸѡ���޸�����
            if (File.Exists(filePath)) File.Delete(filePath);

            var data1 = GenFu.GenFu.ListOf<ExporterHeaderFilterTestData1>();
            var result = await exporter.Export(filePath, data1);
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();

            using (var pck = new ExcelPackage(new FileInfo(filePath)))
            {
                //���ת�����
                var sheet = pck.Workbook.Worksheets.First();
                sheet.Cells["D1"].Value.ShouldBe("name");
                sheet.Dimension.Columns.ShouldBe(4);
            }
            #endregion

            #region ͨ��ɸѡ���޸ĺ�����
            if (File.Exists(filePath)) File.Delete(filePath);
            var data2 = GenFu.GenFu.ListOf<ExporterHeaderFilterTestData2>();
            result = await exporter.Export(filePath, data2);
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();

            using (var pck = new ExcelPackage(new FileInfo(filePath)))
            {
                //���ת�����
                var sheet = pck.Workbook.Worksheets.First();
                sheet.Dimension.Columns.ShouldBe(5);
            }
            #endregion

        }

        [Fact(DisplayName = "DataTable���DTO����Excel")]
        public async Task DynamicExport_Test()
        {
            IExporter exporter = new ExcelExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(DynamicExport_Test) + ".xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var exportDatas = GenFu.GenFu.ListOf<ExportTestDataWithAttrs>(1000);
            var dt = exportDatas.ToDataTable();
            var result = await exporter.Export<ExportTestDataWithAttrs>(filePath, dt);
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
            using (var pck = new ExcelPackage(new FileInfo(filePath)))
            {
                //���ת�����
                var sheet = pck.Workbook.Worksheets.First();
                sheet.Dimension.Columns.ShouldBe(9);
            }
        }

        [Fact(DisplayName = "DataTable����Excel�����趨���֧࣬����ɸѡ���ͱ��֣�")]
        public async Task DynamicDataTableExport_Test()
        {
            IExporter exporter = new ExcelExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(DynamicDataTableExport_Test) + ".xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var exportDatas = GenFu.GenFu.ListOf<ExportTestDataWithAttrs>(50);
            var dt = new DataTable();
            //����������������������
            dt.Columns.Add("Text", Type.GetType("System.String"));
            dt.Columns.Add("Name", Type.GetType("System.String"));
            dt.Columns.Add("Number", Type.GetType("System.Decimal"));
            dt = EntityToDataTable(dt, exportDatas);
            //�Ӹ�ɸѡ������
            var result = await exporter.Export(filePath, dt, new DataTableTestExporterHeaderFilter(), 10);
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
            using (var pck = new ExcelPackage(new FileInfo(filePath)))
            {
                //�ж�Sheet���
                pck.Workbook.Worksheets.Count.ShouldBe(5);
                //���ת�����
                var sheet = pck.Workbook.Worksheets.First();
                sheet.Cells["C1"].Value.ShouldBe("��ֵ");
                sheet.Dimension.Columns.ShouldBe(3);
            }
        }

        [Fact(DisplayName = "�������ݵ���Excel")]
        public async Task Export_Test()
        {
            IExporter exporter = new ExcelExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(Export_Test) + ".xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var result = await exporter.Export(filePath, GenFu.GenFu.ListOf<ExportTestData>(100000));
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }

        [Fact(DisplayName = "DTO����")]
        public async Task ExportAsByteArray_Test()
        {
            IExporter exporter = new ExcelExporter();

            var filePath = GetTestFilePath($"{nameof(ExportAsByteArray_Test)}.xlsx");

            DeleteFile(filePath);

            var result = await exporter.ExportAsByteArray(GenFu.GenFu.ListOf<ExportTestDataWithAttrs>());
            result.ShouldNotBeNull();
            result.Length.ShouldBeGreaterThan(0);
            File.WriteAllBytes(filePath, result);
            File.Exists(filePath).ShouldBeTrue();
        }

        [Fact(DisplayName = "ͨ��Dto������ͷ")]
        public async Task ExportHeaderAsByteArray_Test()
        {
            IExporter exporter = new ExcelExporter();

            var filePath = GetTestFilePath($"{nameof(ExportHeaderAsByteArray_Test)}.xlsx");

            DeleteFile(filePath);

            var result = await exporter.ExportHeaderAsByteArray(GenFu.GenFu.New<ExportTestDataWithAttrs>());
            result.ShouldNotBeNull();
            result.Length.ShouldBeGreaterThan(0);
            result.ToExcelExportFileInfo(filePath);
            File.Exists(filePath).ShouldBeTrue();

            using (var pck = new ExcelPackage(new FileInfo(filePath)))
            {
                //���ת�����
                var sheet = pck.Workbook.Worksheets.First();
                sheet.Name.ShouldBe("����");
                sheet.Dimension.Columns.ShouldBe(9);
            }
        }

        [Fact(DisplayName = "ͨ����̬��ֵ������ͷ")]
        public async Task ExportHeaderAsByteArrayWithItems_Test()
        {
            IExporter exporter = new ExcelExporter();

            var filePath = GetTestFilePath($"{nameof(ExportHeaderAsByteArrayWithItems_Test)}.xlsx");

            DeleteFile(filePath);
            var arr = new[] { "Name1", "Name2", "Name3", "Name4", "Name5", "Name6" };
            var sheetName = "Test";
            var result = await exporter.ExportHeaderAsByteArray(arr, sheetName);
            result.ShouldNotBeNull();
            result.Length.ShouldBeGreaterThan(0);
            result.ToExcelExportFileInfo(filePath);
            File.Exists(filePath).ShouldBeTrue();
            using (var pck = new ExcelPackage(new FileInfo(filePath)))
            {
                //���ת�����
                var sheet = pck.Workbook.Worksheets.First();
                sheet.Name.ShouldBe(sheetName);
                sheet.Dimension.Columns.ShouldBe(arr.Length);
            }
        }

        [Fact(DisplayName = "�����ݶ�̬�е���Excel", Skip = "ʱ��̫����Ĭ������")]
        public async Task LargeDataDynamicExport_Test()
        {
            IExporter exporter = new ExcelExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(LargeDataDynamicExport_Test) + ".xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var exportDatas = GenFu.GenFu.ListOf<ExportTestDataWithAttrs>(1200000);

            var dt = new DataTable();
            //����������������������
            dt.Columns.Add("Text", Type.GetType("System.String"));
            dt.Columns.Add("Name", Type.GetType("System.String"));
            dt.Columns.Add("Number", Type.GetType("System.Decimal"));
            dt = EntityToDataTable(dt, exportDatas);

            var result = await exporter.Export(filePath, dt, maxRowNumberOnASheet: 100000);
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
            using (var pck = new ExcelPackage(new FileInfo(filePath)))
            {
                //�ж�Sheet���
                pck.Workbook.Worksheets.Count.ShouldBe(12);
            }
        }

        [Fact(DisplayName = "Excelģ�嵼���̲Ķ�����ϸ����")]
        public async Task ExportByTemplate_Test()
        {
            //ģ��·��
            var tplPath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "ExportTemplates",
                "2020�괺���̲Ķ�����ϸ����.xlsx");
            //����Excel��������
            IExportFileByTemplate exporter = new ExcelExporter();
            //����·��
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(ExportByTemplate_Test) + ".xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);
            //����ģ�嵼��
            await exporter.ExportByTemplate(filePath,
                new TextbookOrderInfo("����������Ϣ�Ƽ����޹�˾", "���ϳ�ɳ��´��", "ѩ��", "1367197xxxx", null, DateTime.Now.ToLongDateString(),
                    new List<BookInfo>()
                    {
                        new BookInfo(1, "0000000001", "��XX�����ŵ�������", null, "��е��ҵ������", "3.14", 100, "��ע"),
                        new BookInfo(2, "0000000002", "��XX�����ŵ�������", "����", "��е��ҵ������", "3.14", 100, null),
                        new BookInfo(3, null, "��XX�����ŵ�������", "����", "��е��ҵ������", "3.14", 100, "��ע")
                    }),
                tplPath);
        }

        [Fact(DisplayName = "Excelģ���������")]
        public async Task ExportByTemplate_Large_Test()
        {
            //����5000�����ݲ�����1��
            var tplPath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "ExportTemplates",
                "2020�괺���̲Ķ�����ϸ����.xlsx");
            IExportFileByTemplate exporter = new ExcelExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(ExportByTemplate_Large_Test) + ".xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var books = new List<BookInfo>();
            for (int i = 0; i < 5000; i++)
            {
                books.Add(new BookInfo(i + 1, "000000000" + i, "��XX�����ŵ�������", "����", "��е��ҵ������", "3.14", 100 + i, "��ע"));
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await exporter.ExportByTemplate(filePath, new TextbookOrderInfo("����������Ϣ�Ƽ����޹�˾", "���ϳ�ɳ��´��", "ѩ��", "1367197xxxx", "ѩ��", DateTime.Now.ToLongDateString(), books), tplPath);
            stopwatch.Stop();
            //ִ��ʱ�䲻�ó���1�루��ʵ��ִ�л�������Ӱ�죩,�ڲ��Թ������������ձ�С��400ms
            stopwatch.ElapsedMilliseconds.ShouldBeLessThanOrEqualTo(1000);

        }

        [Fact(DisplayName = "�����Զ��嵼������")]
        public async Task ExportTestDataWithoutExcelExporter_Test()
        {
            IExporter exporter = new ExcelExporter();
            var filePath = GetTestFilePath($"{nameof(ExportTestDataWithoutExcelExporter_Test)}.xlsx");
            DeleteFile(filePath);

            var result = await exporter.Export(filePath,
                GenFu.GenFu.ListOf<ExportTestDataWithoutExcelExporter>());
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }
    }
}