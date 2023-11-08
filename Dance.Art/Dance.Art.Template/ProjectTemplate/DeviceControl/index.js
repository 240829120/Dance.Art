import { OutputScriptServiceWrapper } from './DANCE_ART/Output/index'
import { DeviceReceiveBufferDataEventArgsWrapper, DeviceScriptServiceWrapper } from './DANCE_ART/Device/index'
import { UdpSourceModelWrapper } from './DANCE_ART/Device/udp'
import { TcpSourceModelWrapper } from './DANCE_ART/Device/tcp'

let outputService = new OutputScriptServiceWrapper();
let deviceService = new DeviceScriptServiceWrapper();

// 根据设备名获取设备宿主对象
let udpHost = deviceService.getDeviceSource("UDP测试设备");
let tcpHost = deviceService.getDeviceSource("TCP测试设备");

// UDP测试
if (udpHost != null) {

    let udpSource = new UdpSourceModelWrapper(udpHost);

    // 注册数据接收事件
    udpSource.onReceiveData(function (host, e) {
        outputService.log(`接收数据 (${e.length}): ${e.bufferString}`);
    });

    // 发送数据
    udpSource.sendString("Hello World! UDP");
}

// TCP测试
if (tcpHost != null) {

    let tcpSource = new TcpSourceModelWrapper(tcpHost);

    // 注册数据接收事件
    tcpSource.onReceiveData(function (host, e) {
        outputService.log(`接收数据 (${e.length}): ${e.bufferString}`);
    });

    // 发送数据
    tcpSource.sendString("Hello World! TCP");
}