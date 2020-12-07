using System;
using ie.delegates.WiFiScans;
using ie.widgets;


namespace ie.models.WiFiScans
{
    public class MAccessPointImpl : IeDelegate {
        private readonly string identifier;
        private readonly int cellType;
        private readonly string ssid;
        private readonly string bSSID;
        private readonly string strength;
        private readonly string capabilities;
        private readonly string password;

        public MAccessPointImpl(
            string identifier, int cellType, string ssid, string bSSID, string strength, string capabilities, string password) { 
            
            this.identifier = ( (null != identifier) && (identifier.Length > 0) ) 
                ? identifier : IeUtils.generateRandomStringViaGuid();   
            this.cellType = cellType;
            this.ssid = ssid;
            this.bSSID = bSSID;
            this.strength = strength;
            this.capabilities = capabilities;
            this.password = password;         
        }

        public string theIdentifier {
            get { return this.identifier; } 
        }
        
        public int theCellType { get => this.cellType; }

        public string theSSID { get => this.ssid; }

        public string theBSSID => this.bSSID;

        public string thePassword { get => this.password; }

        public string theStrength => this.strength;

        public string theCapabilities => this.capabilities;

        public override string ToString() {
            return "MAccessPointImpl {Identifier: " + theIdentifier + ", SSID: " + theSSID + ", BSSID: " + theBSSID 
            + ", Password: " + thePassword + ", Strength: " + theStrength + ", Capabilities: " + theCapabilities + "}";
        }
    }
}
