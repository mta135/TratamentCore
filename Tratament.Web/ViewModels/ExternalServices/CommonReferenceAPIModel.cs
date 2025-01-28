namespace MAIeDosar.API.ApiViewModels.ExternalServices
{
    public class CommonReferenceAPIModel
    {
        /// <summary>
        /// Номер справочника
        /// </summary>
        public short ReferenceId { get; set; }

        /// <summary>
        /// Код внутри конкретного справочника
        /// </summary>
        public int ReferenceCode { get; set; }

        public CommonReferenceAPIModel() { }
        public CommonReferenceAPIModel(short referenceId, int referenceCode) {
            ReferenceId = referenceId;
            ReferenceCode = referenceCode;

            }
    }
}
