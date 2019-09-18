using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Tests.Models;
using Xunit;
using System.IO;
using Shouldly;
using Magicodes.ExporterAndImporter.Core.Extension;
using System.Linq;

namespace Magicodes.ExporterAndImporter.Tests
{
    public class ExcelImporter_Tests
    {
        public IImporter Importer = new ExcelImporter();

        [Fact(DisplayName = "����")]
        public async Task Importer_Test()
        {
            //��һ������

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "Import", "��Ʒ����ģ��.xlsx");
            var import = await Importer.Import<ImportProductDto>(filePath);
            import.ShouldNotBeNull();

            import.HasError.ShouldBeFalse();
            import.Data.ShouldNotBeNull();
            import.Data.Count.ShouldBeGreaterThanOrEqualTo(2);
            foreach (var item in import.Data)
            {
                if (item.Name.Contains("�ո����"))
                {
                    item.Name.ShouldBe(item.Name.Trim());
                }
                if (item.Code.Contains("��ȥ���ո����"))
                {
                    item.Code.ShouldContain(" ");
                }
                //ȥ���м�ո����
                item.BarCode.ShouldBe("123123");
            }
            //��Ϊ�����Ͳ���
            import.Data[4].Weight.HasValue.ShouldBe(true);
            import.Data[5].Weight.HasValue.ShouldBe(false);
            //��ȡ�Ա�ʽ����
            import.Data[0].Sex.ShouldBe("Ů");
            //��ȡ��ǰ�����Լ��������Ͳ���  ���ʱ�䲻�ԣ���򿪶�Ӧ��Excel���ɸ���Ϊ��ǰʱ�䣬Ȼ�������д˵�Ԫ����
            //import.Data[0].FormulaTest.Date.ShouldBe(DateTime.Now.Date);
            //��ֵ����
            import.Data[0].DeclareValue.ShouldBe(123123);
            import.Data[0].Name.ShouldBe("1212");
            import.Data[0].BarCode.ShouldBe("123123");
            import.Data[1].Name.ShouldBe("12312312");
            import.Data[2].Name.ShouldBe("���ո����");
        }

        [Fact(DisplayName = "����ģ��")]
        public async Task GenerateTemplate_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "testTemplate.xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var result = await Importer.GenerateTemplate<ImportProductDto>(filePath);
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }

        [Fact(DisplayName = "��������")]
        public async Task IsRequired_Test()
        {
            var pros = typeof(ImportProductDto).GetProperties();
            foreach (var item in pros)
            {
                switch (item.Name)
                {
                    //DateTime
                    case "FormulaTest":
                    //int
                    case "DeclareValue":
                    //Required
                    case "Name":
                        item.IsRequired().ShouldBe(true);
                        break;
                    //��Ϊ������
                    case "Weight":
                    //string
                    case "IdNo":
                        item.IsRequired().ShouldBe(false);
                        break;
                }
            }
        }

        [Fact(DisplayName = "ģ�������")]
        public async Task TplError_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "Errors", "ģ���ֶδ���.xlsx");
            var result = await Importer.Import<ImportProductDto>(filePath);
            result.ShouldNotBeNull();
            result.HasError.ShouldBeTrue();
            result.TemplateErrors.Count.ShouldBeGreaterThan(0);
            result.TemplateErrors.Count(p => p.ErrorLevel == Core.Models.ErrorLevels.Error).ShouldBe(1);
            result.TemplateErrors.Count(p => p.ErrorLevel == Core.Models.ErrorLevels.Warning).ShouldBe(1);
        }

        [Fact(DisplayName = "���ݴ�����")]
        public async Task RowDataError_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "Errors", "���ݴ���.xlsx");
            var result = await Importer.Import<ImportProductDto>(filePath);
            result.ShouldNotBeNull();
            result.HasError.ShouldBeTrue();

            result.TemplateErrors.Count.ShouldBe(0);
            result.RowErrors.ShouldContain(p => p.RowIndex == 2 && p.FieldErrors.ContainsKey("��Ʒ����"));
            result.RowErrors.ShouldContain(p => p.RowIndex == 3 && p.FieldErrors.ContainsKey("��Ʒ����"));

            result.RowErrors.ShouldContain(p => p.RowIndex == 7 && p.FieldErrors.ContainsKey("��Ʒ����"));

            result.RowErrors.ShouldContain(p => p.RowIndex == 3 && p.FieldErrors.ContainsKey("����(KG)"));
            result.RowErrors.ShouldContain(p => p.RowIndex == 4 && p.FieldErrors.ContainsKey("��ʽ����"));
            result.RowErrors.ShouldContain(p => p.RowIndex == 5 && p.FieldErrors.ContainsKey("��ʽ����"));
            result.RowErrors.ShouldContain(p => p.RowIndex == 6 && p.FieldErrors.ContainsKey("��ʽ����"));
            result.RowErrors.ShouldContain(p => p.RowIndex == 7 && p.FieldErrors.ContainsKey("��ʽ����"));

            result.RowErrors.ShouldContain(p => p.RowIndex == 3 && p.FieldErrors.ContainsKey("���֤"));
            result.RowErrors.First(p => p.RowIndex == 3 && p.FieldErrors.ContainsKey("���֤")).FieldErrors.Count.ShouldBe(2);

            result.RowErrors.ShouldContain(p => p.RowIndex == 4 && p.FieldErrors.ContainsKey("���֤"));
            result.RowErrors.ShouldContain(p => p.RowIndex == 5 && p.FieldErrors.ContainsKey("���֤"));

            result.RowErrors.Count.ShouldBeGreaterThan(0);
        }
    }
}
