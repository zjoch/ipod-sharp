using System;
using System.Threading;
using System.Collections;
using System.Runtime.InteropServices;

namespace IPod {

    public  class DeviceEventListener : GLib.Object {

        ~DeviceEventListener() {
            Dispose();
        }

        [Obsolete]
        protected DeviceEventListener(GLib.GType gtype) : base(gtype) {}
        public DeviceEventListener(IntPtr raw) : base(raw) {}


        /*
        [DllImport("ipoddevice")]
        static extern void ipod_device_event_listener_set_global_g_main_context (IntPtr context);
        */
        
        [DllImport("ipoddevice")]
        static extern IntPtr ipod_device_event_listener_new ();

        public DeviceEventListener () : base (IntPtr.Zero) {
            if (GetType () != typeof (DeviceEventListener)) {
                CreateNativeObject (new string [0], new GLib.Value[0]);
                return;
            }
            Raw = ipod_device_event_listener_new ();
        }

        [GLib.CDeclCallback]
        delegate void DeviceRemovedSignalDelegate (IntPtr arg0, IntPtr arg1, IntPtr gch);

        static void DeviceRemovedSignalCallback (IntPtr arg0, IntPtr arg1, IntPtr gch) {
            GLib.Signal sig = ((GCHandle) gch).Target as GLib.Signal;
            if (sig == null)
                throw new Exception("Unknown signal GC handle received " + gch);

            IPod.DeviceRemovedArgs args = new IPod.DeviceRemovedArgs ();
            args.Args = new object[1];
            args.Args[0] = GLib.Marshaller.Utf8PtrToString(arg1);
            IPod.DeviceRemovedHandler handler = (IPod.DeviceRemovedHandler) sig.Handler;
            handler (GLib.Object.GetObject (arg0), args);
        }

        [GLib.CDeclCallback]
        delegate void DeviceRemovedVMDelegate (IntPtr listener, IntPtr udi);

        static DeviceRemovedVMDelegate DeviceRemovedVMCallback;

        static void deviceremoved_cb (IntPtr listener, IntPtr udi) {
            DeviceEventListener obj = GLib.Object.GetObject (listener, false) as DeviceEventListener;
            obj.OnDeviceRemoved (GLib.Marshaller.Utf8PtrToString(udi));
        }

        private static void OverrideDeviceRemoved (GLib.GType gtype) {
            if (DeviceRemovedVMCallback == null)
                DeviceRemovedVMCallback = new DeviceRemovedVMDelegate (deviceremoved_cb);
            OverrideVirtualMethod (gtype, "device-removed", DeviceRemovedVMCallback);
        }

        [GLib.DefaultSignalHandler(Type=typeof(IPod.DeviceEventListener), ConnectionMethod="OverrideDeviceRemoved")]
        protected virtual void OnDeviceRemoved (string udi) {
            GLib.Value ret = GLib.Value.Empty;
            GLib.ValueArray inst_and_params = new GLib.ValueArray (2);
            GLib.Value[] vals = new GLib.Value [2];
            vals [0] = new GLib.Value (this);
            inst_and_params.Append (vals [0]);
            vals [1] = new GLib.Value (udi);
            inst_and_params.Append (vals [1]);
            g_signal_chain_from_overridden (inst_and_params.ArrayPtr, ref ret);
            foreach (GLib.Value v in vals)
                v.Dispose ();
        }

        [GLib.Signal("device-removed")]
        public event IPod.DeviceRemovedHandler DeviceRemoved {
            add {
                GLib.Signal sig = GLib.Signal.Lookup (this, "device-removed", new DeviceRemovedSignalDelegate(DeviceRemovedSignalCallback));
                sig.AddDelegate (value);
            }
            remove {
                GLib.Signal sig = GLib.Signal.Lookup (this, "device-removed", new DeviceRemovedSignalDelegate(DeviceRemovedSignalCallback));
                sig.RemoveDelegate (value);
            }
        }

        [GLib.CDeclCallback]
        delegate void DeviceAddedSignalDelegate (IntPtr arg0, IntPtr arg1, IntPtr gch);

        static void DeviceAddedSignalCallback (IntPtr arg0, IntPtr arg1, IntPtr gch) {
            GLib.Signal sig = ((GCHandle) gch).Target as GLib.Signal;
            if (sig == null)
                throw new Exception("Unknown signal GC handle received " + gch);

            IPod.DeviceAddedArgs args = new IPod.DeviceAddedArgs ();
            args.Args = new object[1];
            args.Args[0] = GLib.Marshaller.Utf8PtrToString(arg1);
            IPod.DeviceAddedHandler handler = (IPod.DeviceAddedHandler) sig.Handler;
            handler (GLib.Object.GetObject (arg0), args);
        }

        [GLib.CDeclCallback]
        delegate void DeviceAddedVMDelegate (IntPtr listener, IntPtr udi);

        static DeviceAddedVMDelegate DeviceAddedVMCallback;

        static void deviceadded_cb (IntPtr listener, IntPtr udi) {
            DeviceEventListener obj = GLib.Object.GetObject (listener, false) as DeviceEventListener;
            obj.OnDeviceAdded (GLib.Marshaller.Utf8PtrToString(udi));
        }

        private static void OverrideDeviceAdded (GLib.GType gtype) {
            if (DeviceAddedVMCallback == null)
                DeviceAddedVMCallback = new DeviceAddedVMDelegate (deviceadded_cb);
            OverrideVirtualMethod (gtype, "device-added", DeviceAddedVMCallback);
        }

        [GLib.DefaultSignalHandler(Type=typeof(IPod.DeviceEventListener), ConnectionMethod="OverrideDeviceAdded")]
        protected virtual void OnDeviceAdded (string udi) {
            GLib.Value ret = GLib.Value.Empty;
            GLib.ValueArray inst_and_params = new GLib.ValueArray (2);
            GLib.Value[] vals = new GLib.Value [2];
            vals [0] = new GLib.Value (this);
            inst_and_params.Append (vals [0]);
            vals [1] = new GLib.Value (udi);
            inst_and_params.Append (vals [1]);
            g_signal_chain_from_overridden (inst_and_params.ArrayPtr, ref ret);
            foreach (GLib.Value v in vals)
                v.Dispose ();
        }

        [GLib.Signal("device-added")]
        public event IPod.DeviceAddedHandler DeviceAdded {
            add {
                GLib.Signal sig = GLib.Signal.Lookup (this, "device-added", new DeviceAddedSignalDelegate(DeviceAddedSignalCallback));
                sig.AddDelegate (value);
            }
            remove {
                GLib.Signal sig = GLib.Signal.Lookup (this, "device-added", new DeviceAddedSignalDelegate(DeviceAddedSignalCallback));
                sig.RemoveDelegate (value);
            }
        }

        [GLib.CDeclCallback]
        delegate void NotifyDeviceAddedVMDelegate (IntPtr listener);

        static NotifyDeviceAddedVMDelegate NotifyDeviceAddedVMCallback;

        static void notifydeviceadded_cb (IntPtr listener) {
            DeviceEventListener obj = GLib.Object.GetObject (listener, false) as DeviceEventListener;
            obj.OnNotifyDeviceAdded ();
        }

        private static void OverrideNotifyDeviceAdded (GLib.GType gtype) {
            if (NotifyDeviceAddedVMCallback == null)
                NotifyDeviceAddedVMCallback = new NotifyDeviceAddedVMDelegate (notifydeviceadded_cb);
            OverrideVirtualMethod (gtype, "notify-device-added", NotifyDeviceAddedVMCallback);
        }

        [GLib.DefaultSignalHandler(Type=typeof(IPod.DeviceEventListener), ConnectionMethod="OverrideNotifyDeviceAdded")]
        protected virtual void OnNotifyDeviceAdded () {
            GLib.Value ret = GLib.Value.Empty;
            GLib.ValueArray inst_and_params = new GLib.ValueArray (1);
            GLib.Value[] vals = new GLib.Value [1];
            vals [0] = new GLib.Value (this);
            inst_and_params.Append (vals [0]);
            g_signal_chain_from_overridden (inst_and_params.ArrayPtr, ref ret);
            foreach (GLib.Value v in vals)
                v.Dispose ();
        }

        [GLib.Signal("notify-device-added")]
        public event System.EventHandler NotifyDeviceAdded {
            add {
                GLib.Signal sig = GLib.Signal.Lookup (this, "notify-device-added");
                sig.AddDelegate (value);
            }
            remove {
                GLib.Signal sig = GLib.Signal.Lookup (this, "notify-device-added");
                sig.RemoveDelegate (value);
            }
        }

        [GLib.CDeclCallback]
        delegate void NotifyDeviceRemovedVMDelegate (IntPtr listener);

        static NotifyDeviceRemovedVMDelegate NotifyDeviceRemovedVMCallback;

        static void notifydeviceremoved_cb (IntPtr listener) {
            DeviceEventListener obj = GLib.Object.GetObject (listener, false) as DeviceEventListener;
            obj.OnNotifyDeviceRemoved ();
        }

        private static void OverrideNotifyDeviceRemoved (GLib.GType gtype) {
            if (NotifyDeviceRemovedVMCallback == null)
                NotifyDeviceRemovedVMCallback = new NotifyDeviceRemovedVMDelegate (notifydeviceremoved_cb);
            OverrideVirtualMethod (gtype, "notify-device-removed", NotifyDeviceRemovedVMCallback);
        }

        [GLib.DefaultSignalHandler(Type=typeof(IPod.DeviceEventListener), ConnectionMethod="OverrideNotifyDeviceRemoved")]
        protected virtual void OnNotifyDeviceRemoved () {
            GLib.Value ret = GLib.Value.Empty;
            GLib.ValueArray inst_and_params = new GLib.ValueArray (1);
            GLib.Value[] vals = new GLib.Value [1];
            vals [0] = new GLib.Value (this);
            inst_and_params.Append (vals [0]);
            g_signal_chain_from_overridden (inst_and_params.ArrayPtr, ref ret);
            foreach (GLib.Value v in vals)
                v.Dispose ();
        }

        [GLib.Signal("notify-device-removed")]
        public event System.EventHandler NotifyDeviceRemoved {
            add {
                GLib.Signal sig = GLib.Signal.Lookup (this, "notify-device-removed");
                sig.AddDelegate (value);
            }
            remove {
                GLib.Signal sig = GLib.Signal.Lookup (this, "notify-device-removed");
                sig.RemoveDelegate (value);
            }
        }

        [DllImport("ipoddevice")]
        static extern IntPtr ipod_device_event_listener_get_type();

        public static new GLib.GType GType { 
            get {
                IntPtr raw_ret = ipod_device_event_listener_get_type();
                GLib.GType ret = new GLib.GType(raw_ret);
                return ret;
            }
        }

        static DeviceEventListener () {
            Initializer.Init ();
        }
    }
}
