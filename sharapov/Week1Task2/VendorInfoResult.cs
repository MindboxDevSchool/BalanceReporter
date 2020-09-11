using System;

namespace Week1Task2 {
    //class query result 
    public class VendorInfoResult {
        public string VendorName { get; set; }
        public double TotalSum { get; set; }

        protected bool Equals(VendorInfoResult other) {
            return VendorName == other.VendorName && TotalSum.Equals(other.TotalSum);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((VendorInfoResult) obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(VendorName, TotalSum);
        }
    }
}