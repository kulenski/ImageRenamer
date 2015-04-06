using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eu.kulenski.appkitchen.ImageRenamer {

    public class HistoryItem {
        private const String TAG = "HistoryItem";
        private String _mUsedPrefix;
        private int _mRackNumber;
        private int _mRackSubNumber;
        private int _mViewNumber;
        private ImageHolder.ClickType _type;

        // Constructor
        public HistoryItem(String prefix, int rackNumber, int rackSubNumber, 
                                    int viewNumber, ImageHolder.ClickType type) {

            if (prefix != null) this._mUsedPrefix = prefix;
            else throw new ArgumentNullException(TAG + " -> Argument cannot be null!");

            this._mRackNumber = rackNumber;
            this._mRackSubNumber = rackSubNumber;
            this._mViewNumber = viewNumber;
            this._type = type;
        }

        // public get methods
        public String getPrefix() { return this._mUsedPrefix; }
        public int getRackNumber() { return this._mRackNumber; }
        public int getRackSubNumber() { return this._mRackSubNumber; }
        public int getViewNumber() { return this._mViewNumber; }
    }
}
