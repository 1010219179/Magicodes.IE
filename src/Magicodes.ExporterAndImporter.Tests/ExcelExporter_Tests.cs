// ======================================================================
// 
//           Copyright (C) 2019-2030 ����������Ϣ�Ƽ����޹�˾
//           All rights reserved
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

using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Excel.Builder;
using Magicodes.ExporterAndImporter.Tests.Models;
using Shouldly;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Magicodes.ExporterAndImporter.Tests
{
    public class ExcelExporter_Tests : TestBase
    {
        [Fact(DisplayName = "���Ե���")]
        public async Task AttrsExport_Test()
        {
            IExporter exporter = new ExcelExporter();

            var filePath = GetTestFilePath($"{nameof(AttrsExport_Test)}.xlsx");

            DeleteFile(filePath);

            var result = await exporter.Export(filePath,
                GenFu.GenFu.ListOf<ExportTestDataWithAttrs>());
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
            await File.WriteAllBytesAsync(filePath, result);
            File.Exists(filePath).ShouldBeTrue();
        }

        [Fact(DisplayName = "ExportHeaderAsByteArrayWithItems_Test")]
        public async Task ExportHeaderAsByteArrayWithItems_Test()
        {
            IExporter exporter = new ExcelExporter();

            var filePath = GetTestFilePath($"{nameof(ExportHeaderAsByteArrayWithItems_Test)}.xlsx");

            DeleteFile(filePath);

            var result = await exporter.ExportHeaderAsByteArray(new string[] { "Name1", "Name2", "Name3", "Name4", "Name5", "Name6", }, "Test");
            result.ShouldNotBeNull();
            result.Length.ShouldBeGreaterThan(0);
            await File.WriteAllBytesAsync(filePath, result);
            File.Exists(filePath).ShouldBeTrue();
            //TODO:Excel��ȡ����֤
        }

        [Fact(DisplayName = "ExportHeaderAsByteArray_Test")]
        public async Task ExportHeaderAsByteArray_Test()
        {
            IExporter exporter = new ExcelExporter();

            var filePath = GetTestFilePath($"{nameof(ExportHeaderAsByteArray_Test)}.xlsx");

            DeleteFile(filePath);

            var result = await exporter.ExportHeaderAsByteArray<ExportTestDataWithAttrs>(GenFu.GenFu.New<ExportTestDataWithAttrs>());
            result.ShouldNotBeNull();
            result.Length.ShouldBeGreaterThan(0);
            await File.WriteAllBytesAsync(filePath, result);
            File.Exists(filePath).ShouldBeTrue();
        }

        [Fact(DisplayName = "���������Ե���")]
        public async Task AttrsLocalizationExport_Test()
        {
            IExporter exporter = new ExcelExporter();
            ExcelBuilder.Create().WithColumnHeaderStringFunc(key =>
            {
                if (key.Contains("�ı�"))
                {
                    return "Text";
                }

                return "δ֪����";
            }).Build();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "testAttrsLocalization.xlsx");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            var data = GenFu.GenFu.ListOf<AttrsLocalizationTestData>();
            var result = await exporter.Export(filePath, data);
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }

        [Fact(DisplayName = "�������ݵ���Excel")]
        public async Task Export_Test()
        {
            IExporter exporter = new ExcelExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(Export_Test) + ".xlsx");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            var result = await exporter.Export(filePath, GenFu.GenFu.ListOf<ExportTestData>(100000));
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }
    }
}