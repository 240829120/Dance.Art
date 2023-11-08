import { OutputScriptServiceWrapper } from './DANCE_ART/Output/index'
import { DeviceReceiveBufferDataEventArgsWrapper, DeviceScriptServiceWrapper } from './DANCE_ART/Device/index'
import { UdpSourceModelWrapper } from './DANCE_ART/Device/udp'

let outputService = new OutputScriptServiceWrapper();
let deviceService = new DeviceScriptServiceWrapper();

// 根据设备名获取设备宿主对象
let deviceHost = deviceService.getDeviceSource("新建连接");
let udpSource = new UdpSourceModelWrapper(deviceHost);

// 注册数据接收事件
udpSource.onReceiveData(function (host, e) {

    outputService.log(`接收数据 (${e.length}): ${e.bufferString}`);

});

// 发送数据
udpSource.sendString("Hello World!");