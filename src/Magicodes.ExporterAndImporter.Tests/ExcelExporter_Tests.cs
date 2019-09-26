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

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Excel.Builder;
using Magicodes.ExporterAndImporter.Pdf;
using Magicodes.ExporterAndImporter.Tests.Models;
using Shouldly;
using Xunit;

namespace Magicodes.ExporterAndImporter.Tests
{
    public class ExcelExporter_Tests
    {
        [Fact(DisplayName = "���Ե���")]
        public async Task AttrsExport_Test()
        {
            IExporter exporter = new ExcelExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "testAttrs.xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var result = await exporter.Export(filePath, new List<ExportTestDataWithAttrs>
            {
                new ExportTestDataWithAttrs
                {
                    Text = "��ʵ��ʵ���մ���",
                    Name = "aa",
                    Number = 5000,
                    Text2 = "w��������������",
                    Text3 = "sadsad�򷢴�ʿ����"
                },
                new ExportTestDataWithAttrs
                {
                    Text = "��ʵ��ʵ���մ���",
                    Name = "��ʵ��ʵ���մ���",
                    Number = 6000,
                    Text2 = "w��������������",
                    Text3 = "sadsad�򷢴�ʿ����"
                },
                new ExportTestDataWithAttrs
                {
                    Text = "��ʵ��ʵ�ٶȴ��մ���",
                    Name = "��������",
                    Number = 6000,
                    Text2 = "ͻȻ��Ҳ������",
                    Text3 = "sadsad�򷢴�ʿ����"
                }
            });
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }

        [Fact(DisplayName = "���������Ե���")]
        public async Task AttrsLocalizationExport_Test()
        {
            IExporter exporter = new ExcelExporter();
            ExcelBuilder.Create().WithColumnHeaderStringFunc(key =>
            {
                if (key.Contains("�ı�")) return "Text";
                return "δ֪����";
            }).Build();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "testAttrsLocalization.xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var result = await exporter.Export(filePath, new List<AttrsLocalizationTestData>
            {
                new AttrsLocalizationTestData
                {
                    Text = "��ʵ��ʵ���մ���",
                    Name = "aa",
                    Number = 5000,
                    Text2 = "w��������������",
                    Text3 = "sadsad�򷢴�ʿ����"
                },
                new AttrsLocalizationTestData
                {
                    Text = "��ʵ��ʵ���մ���",
                    Name = "��ʵ��ʵ���մ���",
                    Number = 6000,
                    Text2 = "w��������������",
                    Text3 = "sadsad�򷢴�ʿ����"
                },
                new AttrsLocalizationTestData
                {
                    Text = "��ʵ��ʵ�ٶȴ��մ���",
                    Name = "��������",
                    Number = 6000,
                    Text2 = "ͻȻ��Ҳ������",
                    Text3 = "sadsad�򷢴�ʿ����"
                }
            });
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }

        [Fact(DisplayName = "����Excel")]
        public async Task Export_Test()
        {
            IExporter exporter = new ExcelExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "test.xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var result = await exporter.Export(filePath, new List<ExportTestData>
            {
                new ExportTestData
                {
                    Name1 = "1",
                    Name2 = "test",
                    Name3 = "12",
                    Name4 = "11"
                },
                new ExportTestData
                {
                    Name1 = "1",
                    Name2 = "test",
                    Name3 = "12",
                    Name4 = "11"
                }
            });
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }


        [Fact(DisplayName = "����PDF����")]
        public async Task ExportPDF_Test()
        {
            var exporter = new PdfExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "test.pdf");
            if (File.Exists(filePath)) File.Delete(filePath);
            var result = await exporter.ExportByTemplate(filePath, new List<ExportTestData>
            {
                new ExportTestData
                {
                    Name1 = "1",
                    Name2 = "test",
                    Name3 = "12",
                    Name4 = "11"
                },
                new ExportTestData
                {
                    Name1 = "1",
                    Name2 = "test",
                    Name3 = "12",
                    Name4 = "11"
                }
            });
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }
    }
}