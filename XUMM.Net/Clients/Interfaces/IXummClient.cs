namespace XUMM.Net.Clients.Interfaces
{
    public interface IXummClient
    {
        /// <summary>
        /// Miscellaneous endpoints
        /// </summary>
        public IXummMiscClient Misc { get; }

        /// <summary>
        /// Payload endpoints
        /// </summary>
        public IXummPayloadClient Payload { get; }

        /// <summary>
        /// xApp endpoints
        /// </summary>
        IXummXAppClient XApps { get; }
    }
}
