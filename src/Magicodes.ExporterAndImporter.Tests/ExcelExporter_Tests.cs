using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Tests.Models;
using Xunit;
using System.IO;
using Shouldly;
using Magicodes.ExporterAndImporter.Excel.Builder;

namespace Magicodes.ExporterAndImporter.Tests
{
    public class ExcelExporter_Tests
    {
        public IExporter Exporter = new ExcelExporter();

        [Fact(DisplayName = "����")]
        public async Task Export_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "test.xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var result = await Exporter.Export(filePath, new List<ExportTestData>()
            {
                new ExportTestData()
                {
                    Name1 = "1",
                    Name2 = "test",
                    Name3 = "12",
                    Name4 = "11",
                },
                new ExportTestData()
                {
                    Name1 = "1",
                    Name2 = "test",
                    Name3 = "12",
                    Name4 = "11",
                }
            });
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }

        [Fact(DisplayName = "���Ե���")]
        public async Task AttrsExport_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "testAttrs.xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var result = await Exporter.Export(filePath, new List<ExportTestDataWithAttrs>()
            {
                new ExportTestDataWithAttrs()
                {
                    Text = "��ʵ��ʵ���մ���",
                    Name="aa",
                    Number =5000,
                    Text2 = "w��������������",
                    Text3 = "sadsad�򷢴�ʿ����"
                },
               new ExportTestDataWithAttrs()
                {
                    Text = "��ʵ��ʵ���մ���",
                    Name="��ʵ��ʵ���մ���",
                    Number =6000,
                    Text2 = "w��������������",
                    Text3 = "sadsad�򷢴�ʿ����"
                },
               new ExportTestDataWithAttrs()
                {
                    Text = "��ʵ��ʵ�ٶȴ��մ���",
                    Name="��������",
                    Number =6000,
                    Text2 = "ͻȻ��Ҳ������",
                    Text3 = "sadsad�򷢴�ʿ����"
                },
            });
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }

        [Fact(DisplayName = "���������Ե���")]
        public async Task AttrsLocalizationExport_Test()
        {
            ExcelBuilder.Create().WithLocalStringFunc((key) =>
            {
                if (key.Contains("�ı�"))
                {
                    return "Text";
                }
                return "δ֪����";
            }).Build();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "testAttrsLocalization.xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var result = await Exporter.Export(filePath, new List<AttrsLocalizationTestData>()
            {
                new AttrsLocalizationTestData()
                {
                    Text = "��ʵ��ʵ���մ���",
                    Name="aa",
                    Number =5000,
                    Text2 = "w��������������",
                    Text3 = "sadsad�򷢴�ʿ����"
                },
               new AttrsLocalizationTestData()
                {
                    Text = "��ʵ��ʵ���մ���",
                    Name="��ʵ��ʵ���մ���",
                    Number =6000,
                    Text2 = "w��������������",
                    Text3 = "sadsad�򷢴�ʿ����"
                },
               new AttrsLocalizationTestData()
                {
                    Text = "��ʵ��ʵ�ٶȴ��մ���",
                    Name="��������",
                    Number =6000,
                    Text2 = "ͻȻ��Ҳ������",
                    Text3 = "sadsad�򷢴�ʿ����"
                },
            });
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }
    }
}
