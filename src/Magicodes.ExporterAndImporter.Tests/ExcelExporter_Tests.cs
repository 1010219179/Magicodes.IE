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

        [Fact(DisplayName = "���Ե���")]
        public async Task AttrsExport_Test()
        {
            IExporter exporter = new ExcelExporter();

            var filePath = GetTestFilePath($"{nameof(AttrsExport_Test)}.xlsx");

            DeleteFile(filePath);

            var data = GenFu.GenFu.ListOf<ExportTestDataWithAttrs>(100);
            foreach (var item in data)
            {
                item.LongNo = long.MaxValue;
            }
            var result = await exporter.Export(filePath, data);

            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
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
            }
        }

        //[Fact(DisplayName = "���������Ե���")]
        //public async Task AttrsLocalizationExport_Test()
        //{
        //    IExporter exporter = new ExcelExporter();
        //    ExcelBuilder.Create().WithColumnHeaderStringFunc(key =>
        //    {
        //        if (key.Contains("�ı�")) return "Text";

        //        return "δ֪����";
        //    }).Build();

        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "testAttrsLocalization.xlsx");
        //    if (File.Exists(filePath)) File.Delete(filePath);

        //    var data = GenFu.GenFu.ListOf<AttrsLocalizationTestData>();
        //    var result = await exporter.Export(filePath, data);
        //    result.ShouldNotBeNull();
        //    File.Exists(filePath).ShouldBeTrue();
        //}

        //[Fact(DisplayName = "��̬�е���Excel")]
        //public async Task DynamicExport_Test()
        //{
        //    IExporter exporter = new ExcelExporter();
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(DynamicExport_Test) + ".xlsx");
        //    if (File.Exists(filePath)) File.Delete(filePath);

        //    var exportDatas = GenFu.GenFu.ListOf<ExportTestDataWithAttrs>(1000);

        //    var dt = new DataTable();
        //    //2.����������������������(���ַ�ʽ��ѡ��һ)
        //    dt.Columns.Add("Text", Type.GetType("System.String"));
        //    dt.Columns.Add("Name", Type.GetType("System.String"));
        //    dt.Columns.Add("Number", Type.GetType("System.Decimal"));
        //    dt = EntityToDataTable(dt, exportDatas);

        //    var result = await exporter.Export<ExportTestDataWithAttrs>(filePath, dt);
        //    result.ShouldNotBeNull();
        //    File.Exists(filePath).ShouldBeTrue();

        //    var dt2 = dt.Copy();
        //    var arrResult = await exporter.ExportAsByteArray<ExportTestDataWithAttrs>(dt2);
        //    arrResult.ShouldNotBeNull();
        //    arrResult.Length.ShouldBeGreaterThan(0);
        //    filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(DynamicExport_Test) + "_ByteArray.xlsx");
        //    if (File.Exists(filePath)) File.Delete(filePath);
        //    File.WriteAllBytes(filePath, arrResult);
        //    File.Exists(filePath).ShouldBeTrue();
        //}

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

        [Fact(DisplayName = "ExportAsByteArray_Test")]
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

        //[Fact(DisplayName = "ExportHeaderAsByteArray_Test")]
        //public async Task ExportHeaderAsByteArray_Test()
        //{
        //    IExporter exporter = new ExcelExporter();

        //    var filePath = GetTestFilePath($"{nameof(ExportHeaderAsByteArray_Test)}.xlsx");

        //    DeleteFile(filePath);

        //    var result = await exporter.ExportHeaderAsByteArray(GenFu.GenFu.New<ExportTestDataWithAttrs>());
        //    result.ShouldNotBeNull();
        //    result.Length.ShouldBeGreaterThan(0);
        //    File.WriteAllBytes(filePath, result);
        //    File.Exists(filePath).ShouldBeTrue();
        //}

        //[Fact(DisplayName = "ExportHeaderAsByteArrayWithItems_Test")]
        //public async Task ExportHeaderAsByteArrayWithItems_Test()
        //{
        //    IExporter exporter = new ExcelExporter();

        //    var filePath = GetTestFilePath($"{nameof(ExportHeaderAsByteArrayWithItems_Test)}.xlsx");

        //    DeleteFile(filePath);

        //    var result =
        //        await exporter.ExportHeaderAsByteArray(new[] { "Name1", "Name2", "Name3", "Name4", "Name5", "Name6" },
        //            "Test");
        //    result.ShouldNotBeNull();
        //    result.Length.ShouldBeGreaterThan(0);
        //    File.WriteAllBytes(filePath, result);
        //    File.Exists(filePath).ShouldBeTrue();
        //    //TODO:Excel��ȡ����֤
        //}

        //[Fact(DisplayName = "�����ݶ�̬�е���Excel", Skip = "̫����Ĭ������")]
        //public async Task LargeDataDynamicExport_Test()
        //{
        //    IExporter exporter = new ExcelExporter();
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(LargeDataDynamicExport_Test) + ".xlsx");
        //    if (File.Exists(filePath)) File.Delete(filePath);

        //    var exportDatas = GenFu.GenFu.ListOf<ExportTestDataWithAttrs>(1200000);

        //    var dt = new DataTable();
        //    //����������������������
        //    dt.Columns.Add("Text", Type.GetType("System.String"));
        //    dt.Columns.Add("Name", Type.GetType("System.String"));
        //    dt.Columns.Add("Number", Type.GetType("System.Decimal"));
        //    dt = EntityToDataTable(dt, exportDatas);

        //    var result = await exporter.Export<ExportTestDataWithAttrs>(filePath, dt);
        //    result.ShouldNotBeNull();
        //    File.Exists(filePath).ShouldBeTrue();
        //}

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