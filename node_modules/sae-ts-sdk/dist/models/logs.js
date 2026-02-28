"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SystemLogLevel = void 0;
var SystemLogLevel;
(function (SystemLogLevel) {
    SystemLogLevel[SystemLogLevel["Info"] = 0] = "Info";
    SystemLogLevel[SystemLogLevel["Warning"] = 1] = "Warning";
    SystemLogLevel[SystemLogLevel["Error"] = 2] = "Error";
    SystemLogLevel[SystemLogLevel["Critical"] = 3] = "Critical";
})(SystemLogLevel || (exports.SystemLogLevel = SystemLogLevel = {}));
