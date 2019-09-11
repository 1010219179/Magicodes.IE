using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Tests.Models;
using Xunit;
using System.IO;
using Shouldly;

namespace Magicodes.ExporterAndImporter.Tests
{
    public class ExcelImporter_Tests
    {
        public IImporter Importer = new ExcelImporter();

        [Fact(DisplayName = "����")]
        public async Task Importer_Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Models", "��Ʒ����ģ��.xlsx");
            var import = await Importer.Import<ImportProductDto>(filePath);
            import.ShouldNotBeNull();
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
            }
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
    }
}
