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
    public class ExcelImporter_Tests
    {
        public IImporter Importer = new ExcelImporter();

        [Fact(DisplayName = "����")]
        public async Task Importer_Test()
        {
            var import = await Importer.Import<ImportProductDto>(
                @"D:\Coding\xin-lai.github\src\Magicodes.ExporterAndImporter.Tests\Models\��Ʒ����ģ��.xlsx");
            import.ShouldNotBeNull();
        }
    }
}
