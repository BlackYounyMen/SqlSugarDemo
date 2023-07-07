﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Smart.Utils
{
    /// <summary>
    /// 网络帮助类-ip、计算机名称、域名等
    /// </summary>
    public class NetWorkHelper
    {
        /// <summary>
        /// 获取电脑名称
        /// </summary>
        /// <returns></returns>
        public static string GetComputerName()
        {
            var machineName = Environment.MachineName;
            return machineName;
        }

        /// <summary>
        /// 获取计算机的用户名
        /// </summary>
        /// <returns></returns>
        public static string GetComputerUserName()
        {
            var userName = Environment.UserName;
            return userName;
        }

        /// <summary>
        /// 获取操作系统信息
        /// </summary>
        /// <returns></returns>
        public static string GetOSDesc()
        {
            var osdesc = RuntimeInformation.OSDescription;
            return osdesc;
        }

        /// <summary>
        /// 获取域名
        /// </summary>
        /// <returns></returns>
        public static string GetDomainName()
        {
            //计算机网络连接信息
            var computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            return computerProperties.DomainName;
        }

        /// <summary>
        /// 获取ip   HttpContext方法
        /// </summary>
        /// <returns></returns>
        //public static string GetIp()
        //{
        //    HttpContextAccessor context = new HttpContextAccessor();
        //    var ip = context.HttpContext?.Connection.RemoteIpAddress.ToString();
        //    return ip;
        //}

        /// <summary>
        /// 获取IP对象（包含IPV4、IPV6等信息）
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetIPInfo()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                foreach (var uni in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (uni.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return uni.Address;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 验证IP地址是否合法
        /// </summary>
        /// <param name="ip">要验证的IP地址</param>
        public static bool IsIP(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip)) return false;
            Regex regip = new Regex(@"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$", RegexOptions.IgnoreCase);
            MatchCollection matches = regip.Matches(ip.Trim());
            return matches.Count > 0;
        }

        /// <summary>
        /// 获取MAC拼接的字符串
        /// </summary>
        /// <returns></returns>
        public static string GetMacstring()
        {
            var macs = GetMacsByNetworkInterface();
            var macstr = ListToString(macs, '/');
            return macstr;
        }

        /// <summary>
        /// 获取访问电脑的网卡MAC地址列表（无线网卡、有线网卡）
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMacsByNetworkInterface()
        {
            var macs = new List<string>();
            //获取本地计算机上网络接口的对象
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var tmpInterface in interfaces)
            {
                var up = tmpInterface.OperationalStatus == OperationalStatus.Up;
                var loopback = tmpInterface.NetworkInterfaceType == NetworkInterfaceType.Loopback;
                if (up && !loopback)
                {
                    var address = tmpInterface.GetPhysicalAddress().ToString();
                    var result = Regex.Replace(address, ".{2}", "$0:");
                    var mac = result.Remove(result.Length - 1);
                    macs.Add(mac);
                }
            }
            return macs;
        }

        /// <summary>
        /// list转字符串，并以separator拼接起来
        /// </summary>
        /// <param name="list">list集合</param>
        /// <param name="separator">拼接标识</param>
        /// <returns></returns>
        public static string ListToString(List<string> list, char separator)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                sb.Append(list[i]).Append(separator);
            }
            return sb.ToString().Substring(0, sb.ToString().Length - 1);
        }
    }
}