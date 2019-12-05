// ======================================================================
// 
//           filename : ExcelImporter_Tests.cs
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
using System.Linq;
using System.Threading.Tasks;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Core.Extension;
using Magicodes.ExporterAndImporter.Core.Models;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Tests.Models.Import;
using Newtonsoft.Json;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Magicodes.ExporterAndImporter.Tests
{
    public class ExcelImporter_Tests : TestBase
    {
        public ExcelImporter_Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private readonly ITestOutputHelper _testOutputHelper;
        public IImporter Importer = new ExcelImporter();

        /// <summary>
        /// ����ö��
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "����ѧ�����ݵ���ģ��")]
        public async Task GenerateStudentImportTemplate_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                nameof(GenerateStudentImportTemplate_Test) + ".xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var result = await Importer.GenerateTemplate<ImportStudentDto>(filePath);
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();

            //TODO:��ȡExcel����ͷ�͸�ʽ
        }

        [Fact(DisplayName = "����ģ��")]
        public async Task GenerateTemplate_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(GenerateTemplate_Test) + ".xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var result = await Importer.GenerateTemplate<ImportProductDto>(filePath);
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();

            //TODO:��ȡExcel����ͷ�͸�ʽ
        }

        [Fact(DisplayName = "����ģ���ֽ�")]
        public async Task GenerateTemplateBytes_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(GenerateTemplateBytes_Test) + ".xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);

            var result = await Importer.GenerateTemplateBytes<ImportProductDto>();
            result.ShouldNotBeNull();
            result.Length.ShouldBeGreaterThan(0);
            File.WriteAllBytes(filePath, result);
            File.Exists(filePath).ShouldBeTrue();
        }

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
                if (item.Name.Contains("�ո����")) item.Name.ShouldBe(item.Name.Trim());

                if (item.Code.Contains("��ȥ���ո����")) item.Code.ShouldContain(" ");
                //ȥ���м�ո����
                item.BarCode.ShouldBe("123123");
            }

            //��Ϊ�����Ͳ���
            import.Data.ElementAt(4).Weight.HasValue.ShouldBe(true);
            import.Data.ElementAt(5).Weight.HasValue.ShouldBe(false);
            //��ȡ�Ա�ʽ����
            import.Data.ElementAt(0).Sex.ShouldBe("Ů");
            //��ȡ��ǰ�����Լ��������Ͳ���  ���ʱ�䲻�ԣ���򿪶�Ӧ��Excel���ɸ���Ϊ��ǰʱ�䣬Ȼ�������д˵�Ԫ����
            //import.Data[0].FormulaTest.Date.ShouldBe(DateTime.Now.Date);
            //��ֵ����
            import.Data.ElementAt(0).DeclareValue.ShouldBe(123123);
            import.Data.ElementAt(0).Name.ShouldBe("1212");
            import.Data.ElementAt(0).BarCode.ShouldBe("123123");
            import.Data.ElementAt(1).Name.ShouldBe("12312312");
            import.Data.ElementAt(2).Name.ShouldBe("���ո����");
        }

        [Fact(DisplayName = "�ض����ݲ���")]
        public async Task ImporterDataEnd_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "Import", "�ض����ݲ���.xlsx");
            var import = await Importer.Import<ImportProductDto>(filePath);
            import.ShouldNotBeNull();
            import.Data.ShouldNotBeNull();
            import.Data.Count.ShouldBe(6);
        }

        [Fact(DisplayName = "�ɷ���ˮ�������")]
        public async Task ImportPaymentLogs_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "Import", "�ɷ���ˮ����ģ��.xlsx");
            var import = await Importer.Import<ImportPaymentLogDto>(filePath);
            import.ShouldNotBeNull();
            import.HasError.ShouldBeTrue();
            import.Exception.ShouldBeNull();
            import.Data.Count.ShouldBe(20);
        }

        [Fact(DisplayName = "��������")]
        public async Task IsRequired_Test()
        {
            var pros = typeof(ImportProductDto).GetProperties();
            foreach (var item in pros)
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

        [Fact(DisplayName = "��⵼�����")]
        public async Task QuestionBankImporter_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "Import", "��⵼��ģ��.xlsx");
            var import = await Importer.Import<ImportQuestionBankDto>(filePath);
            import.ShouldNotBeNull();
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(import.RowErrors));
            import.HasError.ShouldBeFalse();
            import.Data.ShouldNotBeNull();
            import.Data.Count.ShouldBe(404);

            #region ���Boolֵӳ��

            //��
            import.Data.ElementAt(0).IsDisorderly.ShouldBeTrue();
            //��
            import.Data.ElementAt(1).IsDisorderly.ShouldBeFalse();
            //��
            import.Data.ElementAt(2).IsDisorderly.ShouldBeTrue();
            //��
            import.Data.ElementAt(3).IsDisorderly.ShouldBeFalse();

            #endregion

            import.RowErrors.Count.ShouldBe(0);
            import.TemplateErrors.Count.ShouldBe(0);
        }

        [Fact(DisplayName = "���ݴ�����")]
        public async Task RowDataError_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "Errors", "���ݴ���.xlsx");
            var result = await Importer.Import<ImportRowDataErrorDto>(filePath);
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
            result.RowErrors.First(p => p.RowIndex == 3 && p.FieldErrors.ContainsKey("���֤")).FieldErrors.Count
                .ShouldBe(3);

            result.RowErrors.ShouldContain(p => p.RowIndex == 4 && p.FieldErrors.ContainsKey("���֤"));
            result.RowErrors.ShouldContain(p => p.RowIndex == 5 && p.FieldErrors.ContainsKey("���֤"));

            #region �ظ�����

            var errorRows = new List<int>()
            {
                5,6
            };
            result.RowErrors.ShouldContain(p =>
                errorRows.Contains(p.RowIndex) && p.FieldErrors.ContainsKey("��Ʒ����") &&
                p.FieldErrors.Values.Contains("���������ظ������飡�����У�5��6��"));

            errorRows = new List<int>()
            {
                8,9,11,13
            };
            result.RowErrors.ShouldContain(p =>
                errorRows.Contains(p.RowIndex) && p.FieldErrors.ContainsKey("��Ʒ����") &&
                p.FieldErrors.Values.Contains("���������ظ������飡�����У�8��9��11��13��"));

            errorRows = new List<int>()
            {
                4,6,8,10,11,13
            };
            result.RowErrors.ShouldContain(p =>
                errorRows.Contains(p.RowIndex) && p.FieldErrors.ContainsKey("��Ʒ�ͺ�") &&
                p.FieldErrors.Values.Contains("���������ظ������飡�����У�4��6��8��10��11��13��"));

            #endregion

            result.RowErrors.Count.ShouldBeGreaterThan(0);

            //һ�н��������һ������
            foreach (var item in result.RowErrors.GroupBy(p => p.RowIndex).Select(p => new { p.Key, Count = p.Count() }))
                item.Count.ShouldBe(1);

            char.Parse(",");
            char.Parse("��");
        }

        [Fact(DisplayName = "ѧ���������ݵ���")]
        public async Task StudentInfoImporter_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "Import", "ѧ���������ݵ���.xlsx");
            var import = await Importer.Import<ImportStudentDto>(filePath);
            import.ShouldNotBeNull();
            if (import.Exception != null) _testOutputHelper.WriteLine(import.Exception.ToString());

            if (import.RowErrors.Count > 0) _testOutputHelper.WriteLine(JsonConvert.SerializeObject(import.RowErrors));
            import.HasError.ShouldBeFalse();
            import.Data.ShouldNotBeNull();
            import.Data.Count.ShouldBe(16);
        }

        [Fact(DisplayName = "ģ�������")]
        public async Task TplError_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "Errors", "ģ���ֶδ���.xlsx");
            var result = await Importer.Import<ImportProductDto>(filePath);
            result.ShouldNotBeNull();
            result.HasError.ShouldBeTrue();
            result.TemplateErrors.Count.ShouldBeGreaterThan(0);
            result.TemplateErrors.Count(p => p.ErrorLevel == ErrorLevels.Error).ShouldBe(1);
            result.TemplateErrors.Count(p => p.ErrorLevel == ErrorLevels.Warning).ShouldBe(1);
        }
    }
}