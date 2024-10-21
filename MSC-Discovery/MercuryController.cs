using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeroconf;

namespace MSC_Discovery
{
    internal class MercuryController
    {

        public String hostName { get; private set; }
        public String ipAddress { get; private set; }
        public String portNumber { get; private set; }
        public String macAddress { get; private set; }
        public String oemCode { get; private set; }
        public String boardType { get; private set; }
        public String firmwareVersion { get; private set; }
        public String serialNumber { get; private set; }
        public String dipStatus { get; private set; }
        public String onlineStatus { get; private set; }
        public String tlsStatus { get; private set; }
        public String isServer { get; private set; }
        public String notes { get; private set; }

        public MercuryController()
        {

        }

        public MercuryController(IZeroconfHost host)
        {
            populateFromHost(host);
        }

        public void populateFromHost(IZeroconfHost host)
        {
            //Mercury Boards will show a single Service, the key is different per controller so this just works better
            IService svc = host.Services[host.Services.FirstOrDefault().Key];
            IReadOnlyDictionary<string, string> props = svc.Properties[0];

            this.hostName = host.DisplayName;
            this.ipAddress = host.IPAddress.ToString();
            this.portNumber = svc.Port.ToString();

            this.macAddress = props["MAC"];
            this.oemCode = props["OEM"];
            this.boardType = this.getBoardType(props["ProdId"], props["ProdVer"]);
            this.firmwareVersion = props["SoftMaj"] + "." + props["SoftMin"] + "." + props["SoftBld"] + "." + props["IntBld"];
            this.serialNumber = props["Serial"];
            this.dipStatus = props["DIP"];
            this.onlineStatus = props["Online"];
            this.tlsStatus = props["TlsStatus"];
            this.isServer = props["IsServer"];
            this.notes = props["Notes"];

        }

        public String getBoardType(string prodId, string prodVer)
        {
            switch(prodId)
            {
                /* SCP Series */
                case "1": 
                    switch(prodVer)
                    {
                        case "0":
                            return "SCP_2";
                            break;
                        case "1":
                            return "SCP_C";
                            break;
                        case "2":
                            return "SCP_E";
                            break;
                        case "3":
                            return "SCP_2";
                            break;
                        case "4":
                            return "SCP_C";
                            break;
                        case "5":
                            return "SCP_E";
                            break;
                        default:
                            return "MSC";
                            break;
                    }
                    break;
                case "2": /* EP Series */
                    switch (prodVer)
                    {
                        case "7":
                            return "EP2500";
                            break;
                        case "8":
                            return "EP1502";
                            break;
                        case "9":
                            return "EP1501";
                            break;
                        case "13":
                            return "M5_IC";
                            break;
                        case "19":
                            return "EP4502";
                            break;
                        case "17":
                            return "MI_RS4";
                            break;
                        case "18":
                            return "MI_XL16";
                            break;
                        case "21":
                            return "RK_ARM";
                            break;
                        case "22":
                            return "MS_ICS";
                            break;
                        case "20":
                            return "NXT_1D";
                            break;
                        case "15":
                            return "NXT_2D";
                            break;
                        case "16":
                            return "NXT_4D";
                            break;
                        default:
                            return "MSC";
                            break;
                    }
                    break;
                case "3": /* LP Series */
                    switch (prodVer)
                    {
                        case "7":
                            return "LP2500";
                            break;
                        case "8":
                            return "LP1502";
                            break;
                        case "9":
                            return "LP1501";
                            break;
                        case "19":
                            return "LP4502";
                            break;
                        case "23":
                            return "AP2";
                            break;
                        case "24":
                            return "SSC";
                            break;
                        default:
                            return "MSC";
                            break;
                    }
                    break;
                case "40":
                    switch (prodVer)
                    {
                        case "8":
                            return "PW6K1IC";
                            break;
                        default:
                            return "MSC";
                            break;
                    }
                    break;
                case "41":
                    switch (prodVer)
                    {
                        case "12":
                            return "PRO32IC";
                            break;
                        default:
                            return "MSC";
                            break;
                    }
                    break;
                default:
                    return "MSC";
                    break;
            }

        }

    }
}
