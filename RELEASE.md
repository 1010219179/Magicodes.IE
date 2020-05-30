# Release Log

#### **2020.05.24**

- **��Nuget���汾���µ�2.2.2**
- **��Excel���롿������stream������չ����**
- **��Excel���������������ݾ��У����о��С�������У�**
- **����������һЩ�м������������޸����Ż�**

#### **2020.05.16**
- **��Nuget���汾���µ�2.2.1**
- **��PDF��������ģ�����������������**


#### **2020.05.12**

- **��Nuget���汾���µ�2.2.0**
- **��Excelģ�嵼����֧�ֵ����ֽ�**
- **���ĵ���Magicodes.IE Csv���뵼��**
- **��Excel���뵼�����޸���ע���������**
- **��������ASP.NET Core Web API ��ʹ���Զ����ʽ�����򵼳�Excel��Pdf��Csv������** [#64](https://github.com/dotnetcore/Magicodes.IE/issues/64)
- **�����뵼����֧��ʹ��System.ComponentModel.DataAnnotations�����ռ��µĲ������������Ƶ��뵼��**  [#63](https://github.com/dotnetcore/Magicodes.IE/issues/63)

#### **2020.04.16**
- **��Nuget���汾���µ�2.2.0-beta9**
- **��Excelģ�嵼�����޸�ֻ����һ��ʱ�ĵ��� [#73](https://github.com/dotnetcore/Magicodes.IE/issues/73)**
- **��Excel���롿֧�ַ��ر�ͷ������ [#76](https://github.com/dotnetcore/Magicodes.IE/issues/76)**
- **��Excel���뵼�롿[#63](https://github.com/dotnetcore/Magicodes.IE/issues/63)**
  - ֧��ʹ��System.ComponentModel.DataAnnotations�����ռ��µĲ������������Ƶ��뵼��������
    - DisplayAttribute
    - DisplayFormatAttribute
    - DescriptionAttribute
  - ��װ�򵥵�����ʹ�õĵ�һ���ԣ�����
    - IEIgnoreAttribute�������������ԡ�ö�ٳ�Ա����Ӱ�쵼��͵�����

#### **2020.04.02**
- **��Nuget���汾���µ�2.2.0-beta8**

- **��Excelģ�嵼����֧��ͼƬ [#62](https://github.com/dotnetcore/Magicodes.IE/issues/62)����Ⱦ�﷨������ʾ��**

 ```
  {{Image::ImageUrl?Width=50&Height=120&Alt=404}}
  {{Image::ImageUrl?w=50&h=120&Alt=404}}
  {{Image::ImageUrl?Alt=404}}
 ```

#### **2020.03.29**
- **��Nuget���汾���µ�2.2.0-beta7**
- **��Excelģ�嵼�����޸���Ⱦ���� [#51](https://github.com/dotnetcore/Magicodes.IE/issues/51)**

#### **2020.03.27**
- **��Nuget���汾���µ�2.2.0-beta6**
- **��Excel���뵼�����޸�.NET Core 2.2�İ��������� [#68](https://github.com/dotnetcore/Magicodes.IE/issues/68)**

#### **2020.03.26**
- **��Nuget���汾���µ�2.2.0-beta4**
- **��Excel��Sheet�������޸�[#66](https://github.com/dotnetcore/Magicodes.IE/issues/66)������ӵ�Ԫ����**

#### **2020.03.25**
- **��Nuget���汾���µ�2.2.0-beta3**
- **��Excel���롿�޸��������� [#68](https://github.com/dotnetcore/Magicodes.IE/issues/68)**
- **��Excel���������ExcelOutputType���ã�֧������޸�ʽ�ĵ�����[#54](https://github.com/dotnetcore/Magicodes.IE/issues/54)����ʹ�ô˷�ʽ��**

#### **2020.03.19**
- **��Nuget���汾���µ�2.2.0-beta2**
- **��Excel���롿�޸����ڸ�ʽ�ĵ���Bug��֧��DateTime��DateTimeOffset�Լ���Ϊ�����ͣ�Ĭ��֧�ֱ��ػ�ʱ���ʽ��Ĭ�ϸ��ݵ����Զ�ʹ�ñ�������ʱ���ʽ��**
- **��Excel���뵼������ӵ�Ԫ����ExportAndImportUseOneDto_Test����ʹ��ͬһ��Dto������������в��ԡ�Issue�� [#53](https://github.com/dotnetcore/Magicodes.IE/issues/53)**

#### **2020.03.18**
- **��Nuget���汾���µ�2.2.0-beta1**
- **��Excel�������������API:**
````csharp

        /// <summary>
        ///     ׷�Ӽ��ϵ���ǰ��������
        ///     append the collection to context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataItems"></param>
        /// <returns></returns>
        ExcelExporter Append<T>(ICollection<T> dataItems) where T : class;

        /// <summary>
        ///     �������е�׷������
        ///     export excel after append all collectioins
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<ExportFileInfo> ExportAppendData(string fileName);

        /// <summary>
        ///     �������е�׷������
        ///     export excel after append all collectioins
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<byte[]> ExportAppendDataAsByteArray();

````

- **��Excel������֧�ֶ��ʵ�嵼�����Sheet**����л@ccccccmd �Ĺ��� [#pr52](https://github.com/dotnetcore/Magicodes.IE/pull/52) ��Issue�� [#50](https://github.com/dotnetcore/Magicodes.IE/issues/50)��ʹ�ô���ο����������Ԫ���ԣ�ExportMutiCollection_Test����

````csharp
            var exporter = new ExcelExporter();
            var list1 = GenFu.GenFu.ListOf<ExportTestDataWithAttrs>();
            var list2 = GenFu.GenFu.ListOf<ExportTestDataWithSplitSheet>(30);
            var result = exporter.Append(list1).Append(list2).ExportAppendData(filePath);
````

#### **2020.03.12**
- **��Nuget���汾���µ�2.1.4**
- **��Excel���롿֧��ͼƬ���룬������ImportImageFieldAttribute**
  - ����ΪBase64
  - ���뵽��ʱĿ¼
  - ���뵽ָ��Ŀ¼
- **��Excel������֧��ͼƬ������������ExportImageFieldAttribute**
  - ���ļ�·������ΪͼƬ
  - ������·������ΪͼƬ

#### **2020.03.06**
- **��Nuget���汾���µ�2.1.3**
- **��Excel���롿�޸�GUID���͵����⡣�������<https://github.com/dotnetcore/Magicodes.IE/issues/44>����**

#### **2020.02.25**
- **��Nuget���汾���µ�2.1.2**
- **�����뵼������֧��CSV**
- **���ĵ�������Pdf�����ĵ�**

#### **2020.02.24**
- **��Nuget���汾���µ�2.1.1-beta**
- **�����롿Excel����֧�ֵ����ע����������ExcelImporterAttribute��ImportDescription���ԣ������ڶ�������Excel����˵��**
- **���ع�����������ӿ�**
  - IExcelExporter���̳���IExporter, IExportFileByTemplate��Excel���е�API���ڴ˲���
  - IExcelImporter���̳���IImporter��Excel���е�API�ڴ˲��䣬���硰ImportMultipleSheet������ImportSameSheets��
- **���ع�������ʵ������ע��**
- **����������ɴ��븲���ʵ�DevOps������**

#### **2020.02.14**
- **��Nuget���汾���µ�2.1.0**
- **��������PDF����֧��.NET 4.6.1���������Ԫ����**

#### **2020.02.13**
- **��Nuget���汾���µ�2.0.2**
- **�����롿�޸����е����Bug����Ԫ���ԡ�OneColumnImporter_Test�����������<https://github.com/dotnetcore/Magicodes.IE/issues/35>����**
- **���������޸�����HTML��Pdf��Wordʱ��ģ����ĳЩ����±��뱨������⡣**
- **�����롿��д���м�顣**

#### **2020.02.11**
- **��Nuget���汾���µ�2.0.0**
- **��������Excelģ�嵼���޸����Table��Ⱦ�Լ��ϲ���Ԫ����Ⱦ�����⣬�������Ԫ���ԡ�ExportByTemplate_Test1�����������<https://github.com/dotnetcore/Magicodes.IE/issues/34>����**
- **������������ģ�嵼���ĵ�Ԫ���ԣ���Ե�����������Ⱦ��飬ȷ�����е�Ԫ�������Ⱦ��**

#### **2020.02.05**
- **��Nuget���汾���µ�2.0.0-beta4**
- **�����롿֧����ɸѡ������ʵ�ֽӿڡ�IImportHeaderFilter�����������ڼ��ݶ����Ե���ȳ������������Ԫ���ԡ�ImportHeaderFilter_Test��**
- **�����롿֧�ִ����ע�ļ�·������������Ĭ��ͬĿ¼"_"��׺����**
- **�����롿���Ƶ�Ԫ���ԡ�ImportResultFilter_Test��**
- **���������޸ġ�ValueMappingAttribute���������ռ�ΪMagicodes.ExporterAndImporter.Core**

#### **2020.02.04**
- **��Nuget���汾���µ�2.0.0-beta2**
- **�����롿֧�ֵ�����ɸѡ������IImportResultFilter�������ڶ����Գ����Ĵ����ע������ʹ�ü���Ԫ���ԡ�ImportResultFilter_Test��**
- **���������޸�IExporterHeaderFilter�������ռ�ΪMagicodes.ExporterAndImporter.Core.Filters**

#### **2020.01.18**
- **��Nuget���汾���µ�2.0.0-beta1**
- **����������ȫ�ع���������Excelģ�鲢����д�󲿷ֽӿ�**
- **��������֧����ͷɸѡ������IExporterHeaderFilter������ʹ�ü���Ԫ����**
- **���������޸�ת��DataTableʱ֧��Ϊ������**
- **������������Excel֧�ֲ��Sheet�������������ԡ�ExporterAttribute���ġ�MaxRowNumberOnASheet����ֵ��Ϊ0�򲻲�֡��������Ԫ����**
- **���������޸���������޷�ɸѡ�����⡣Ŀǰ������Ϊ���ݱ�**
- **�������������չ����ToExcelExportFileInfo**
- **��������IExporter�����������̬DataTable�������������趨��Dto���ɶ�̬�������ݣ�����֧�ֱ�ͷɸѡ����Sheet���**
````csharp
        /// <summary>
        ///     ����Excel
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="dataItems">����</param>
        /// <param name="exporterHeaderFilter">��ͷɸѡ��</param>
        /// <param name="maxRowNumberOnASheet">һ��Sheet��������������������֮��������Sheet</param>
        /// <returns>�ļ�</returns>
        Task<ExportFileInfo> Export(string fileName, DataTable dataItems, IExporterHeaderFilter exporterHeaderFilter = null, int maxRowNumberOnASheet = 1000000);

        /// <summary>
        ///     ����Excel
        /// </summary>
        /// <param name="dataItems">����</param>
        /// <param name="exporterHeaderFilter">��ͷɸѡ��</param>
        /// <param name="maxRowNumberOnASheet">һ��Sheet��������������������֮��������Sheet</param>
        /// <returns>�ļ�����������</returns>
        Task<byte[]> ExportAsByteArray(DataTable dataItems, IExporterHeaderFilter exporterHeaderFilter = null, int maxRowNumberOnASheet = 1000000);
````

#### **2020.01.16**
- **��Nuget���汾���µ�1.4.25**
- **���������޸�û�ж��嵼�����Իᱨ������Σ��������Ԫ���ԡ�ExportTestDataWithoutExcelExporter_Test�����������<https://github.com/dotnetcore/Magicodes.IE/issues/21>����**

#### **2020.01.16**
- **��Nuget���汾���µ�1.4.24**
- **���������޸����ڸ�ʽĬ�ϵ������ֵ�Bug��Ĭ�������yyyy-MM-dd��������ͨ�����á�[ExporterHeader(DisplayName = "����2", Format = "yyyy-MM-dd HH:mm:ss")]�����޸ġ��������<https://github.com/dotnetcore/Magicodes.IE/issues/22>����**

#### **2020.01.14**
- **��Nuget���汾���µ�1.4.21**
- **��������Excelģ�嵼���޸�������ΪNull�����Bug��**

#### **2020.01.09**
- **��Nuget���汾���µ�1.4.20**
- **��������Excelģ�嵼�������Ż���5000���������1������ɣ��������Ԫ����ExportByTemplate_Large_Test��**

#### **2020.01.08**
- **��Nuget���汾���µ�1.4.18**
- **�����롿֧�ֵ��������������**
    - **ImporterAttribute֧��MaxCount���ã�Ĭ��Ϊ50000**
    - **�����ص�Ԫ����**

#### **2020.01.07**
- **��Nuget���汾���µ�1.4.17**
- **���ع����ع�IExportFileByTemplate�е�ExportByTemplate��������htmlTemplate��Ϊtemplate���Ա�֧��Excelģ�嵼����**
- **��������֧��Excelģ�嵼������д��ص�Ԫ���ԣ����ʹ�ü��̡̳�Excelģ�嵼��֮�����̲Ķ�����**
    - **֧�ֵ�Ԫ�񵥸���**
    - **֧���б�**


#### **2019.12.17**
- **��Nuget���汾���µ�1.4.16**
- **�����롿Excel����֧�ֶ�sheet���룬��лtanyongzheng��[https://github.com/dotnetcore/Magicodes.IE/pull/18](https://github.com/dotnetcore/Magicodes.IE/pull/18)��**

#### **2019.12.10**
- **��Nuget���汾���µ�1.4.15**
- **�����ԡ���Ԫ������Ӷ��ܰ汾֧�� (<https://docs.xin-lai.com/2019/12/10/%E6%8A%80%E6%9C%AF%E6%96%87%E6%A1%A3/Magicodes.IE%E7%BC%96%E5%86%99%E5%A4%9A%E6%A1%86%E6%9E%B6%E7%89%88%E6%9C%AC%E6%94%AF%E6%8C%81%E5%92%8C%E6%89%A7%E8%A1%8C%E5%8D%95%E5%85%83%E6%B5%8B%E8%AF%95/>)**
- **���޸����޸�����.NET Framework 461�µ�����**

#### **2019.12.06**
- **��Nuget���汾���µ�1.4.14**
- **���ع��������ع�**
	- **�Ƴ�����δʹ�õĴ���**
	- **��TemplateFileInfo������ΪExportFileInfo**
	- **��IExporterByTemplate�ӿڲ��Ϊ4���ӿڣ�IExportListFileByTemplate, IExportListStringByTemplate, IExportStringByTemplate, IExportFileByTemplate�����޸����ʵ��**
	- **�ع�ImportHelper���ִ���**
- **�����롿�޸�����Excelʱ��ͷ���õ����⣬�ѶԴ˱�д��Ԫ���ԣ�������Ʒ��Ϣ���롿**
- **�����ơ���дExportAsByteArray����DataTable�ĵ�Ԫ���ԣ�ExportWordFileByTemplate_Test**

#### **2019.11.25**
- **��Nuget���汾���µ�1.4.13**
- **��������Pdf����֧���������ã������Ԫ���ԡ����������Ű��վݡ���Ŀǰ��Ҫ֧���������ã�**
	- **Orientation���Ű淽�򣨺��š����ţ�**
	- **PaperKind��ֽ�����ͣ�Ĭ��A4**
	- **IsEnablePagesCount���Ƿ����÷�ҳ��**
	- **Encoding���������ã�Ĭ��UTF8**
	- **IsWriteHtml���Ƿ����HTMLģ�壬������ã�������.html��׺�Ķ�Ӧ��HTML�ļ����������**
	- **HeaderSettings��ͷ�����ã�ͨ����������ͷ���ķ�ҳ���ݺ���Ϣ**
	- **FooterSettings���ײ�����**

#### **2019.11.24**
- **��Nuget���汾���µ�1.4.12**
- **��������������̬��֧�ֳ���100W����ʱ�Զ����Sheet�������PR��[https://github.com/xin-lai/Magicodes.IE/pull/14](https://github.com/xin-lai/Magicodes.IE/pull/14)��**

#### **2019.11.20**
- **��Nuget���汾���µ�1.4.11**
- **���������޸�Datatable�е�˳���DTO��˳��һ�£��������ݷŴ��У������PR��[https://github.com/xin-lai/Magicodes.IE/pull/13](https://github.com/xin-lai/Magicodes.IE/pull/13)��**

#### **2019.11.16**
- **��Nuget���汾���µ�1.4.10**
- **���������޸�Pdf�����ڶ��߳��µ�����**

#### **2019.11.13**
- **��Nuget���汾���µ�1.4.5**
- **���������޸�����Pdf��ĳЩ����¿��ܻᵼ���ڴ汨�������**
- **��������������������վݵ�Ԫ����ʾ��������Ӵ��������������в���**

#### **2019.11.5**
- **��Nuget���汾���µ�1.4.4**
- **�����롿�޸�ö�����͵����⣬����д��Ԫ����**
- **�����롿����ֵӳ�䣬֧��ͨ����ValueMappingAttribute����������ֵӳ���ϵ���������ɵ���ģ���������֤Լ���Լ���������ת����**
- **�����롿�Ż�ö�ٺ�Bool���͵ĵ���������֤������ɣ��Ա���ģ�����ɺ�����ת��**
	- **ö��Ĭ������»��Զ���ȡö�ٵ���������ʾ�������ƺ�ֵ����������**
	- **bool����Ĭ�ϻ����ɡ��ǡ��͡��񡱵�������**
	- **����������Զ���ֵӳ�䣬�򲻻�����Ĭ��ѡ��**
- **�����롿֧��ö�ٿ�Ϊ������**

#### **2019.10.30**
- **��Nuget���汾���µ�1.4.0**
- **��������Excel����֧�ֶ�̬�е���������DataTable������л�����ѣ�https://github.com/xin-lai/Magicodes.IE/pull/8 ��**

#### **2019.10.22**
- **��Nuget���汾���µ�1.3.7**
- **�����롿�޸������е���֤����**
- **�����롿������֤������Ϣ��һ�н��������һ������**
- **�����롿�޸���������ĳЩ����¿����������쳣**
- **�����롿��Ӵ��ں����еĵ��������µĵ�Ԫ����**

#### **2019.10.21**
- **��Nuget���汾���µ�1.3.4**
- **�����롿֧�����ú����У��Ա�����Dto�����������������ӳ��**

#### **2019.10.18**
- **���Ż����Ż�.NET��׼��2.1�¼���תDataTable������**
- **���ع����ദIList<T>�޸�ΪICollection<T>**
- **�����ơ����䲿�ֵ�Ԫ����**

#### **2019.10.12**
- **���ع����ع�HTML��PDF�������߼������޸�IExporterByTemplateΪ��**
  - **Task<string> ExportListByTemplate<T>(IList<T> dataItems, string htmlTemplate = null) where T : class;**
  - **Task<string> ExportByTemplate<T>(T data, string htmlTemplate = null) where T : class;**
- **��ʾ��������վݵ����ĵ�Ԫ����ʾ��**



#### **2019.9.28**
- **���������޸�Ĭ�ϵĵ���HTML��Word��Pdfģ��**
- **�����롿��ӽض��еĵ�Ԫ���ԣ��Բ����м�ո�ͽ�β�ո�**
- **�����롿�������ݴ����⡿�͡����롿��Ԫ���Ե�Dto�ֿ���ȷ��ȫ����Ԫ����ͨ��**
- **���ĵ��������ĵ�**

#### **2019.9.26**
- **��������֧�ֵ���Word��Pdf��HTML��֧���Զ��嵼��ģ��**
- **�������������ص����ĵ�Ԫ����**
- **�����롿֧���ظ���֤��������ImporterHeader���Ե�IsAllowRepeatΪfalse**

#### **2019.9.19**
- **�����롿֧�ֽ�ֹ�����ã���δ������Ĭ�������ո��ֹ**
- **�����롿����֧��ͨ����������Sheet����**

#### **2019.9.18**

- **�����롿�ع�����ģ��**
- **�����롿ͳһ���������Ϣ**
	- **Exception �������쳣��Ϣ**
	- **RowErrors �� ���ݴ�����Ϣ**
	- **TemplateErrors ��ģ�������Ϣ��֧�ִ���ּ�**
	- **HasError : �Ƿ���ڴ��󣨽��������쳣���Ҵ���ȼ�ΪErrorʱ����true��**
- **�����롿�������ͱ����Զ�ʶ�𣬱���int��double�Ȳ���Ϊ�������Զ�ʶ�������������Required**
- **�����롿�޸�Excelģ���Sheet����**
- **�����롿֧�ֵ����ͷλ�����ã�Ĭ��Ϊ1**
- **�����롿֧�������򣨵���ģ�������Ų�����Ҫ�̶���**
- **�����롿֧������������**
- **�����롿֧�ֽ������Excel���д����ע��֧�ֶ������**
- **�����롿��ǿ�Ի������ͺͿ�Ϊ�����͵�֧��**
- **��EPPlus������EPPlus.Core�Ѿ���ά������EPPlus�İ���EPPlus.Core��ΪEPPlus��**

#### **2019.9.11**

- **�����롿����֧���Զ�ȥ��ǰ��ո�Ĭ�����ã���������н��йرգ������AutoTrim����**
- **�����롿����Dto���ֶ���������ImporterHeader��֧��ͨ��DisplayAttribute���Ի�ȡ����**
- **�����롿�����Excel�Ƴ���Sheet���Ƶ�Լ����Ĭ�ϻ�ȡ��һ��Sheet**
- **�����롿�������Ӷ��м�ո�Ĵ���֧�֣�������FixAllSpace**
- **�����롿�������ƶ��������͵�֧��**
- **�����롿���Ƶ���ĵ�Ԫ����**