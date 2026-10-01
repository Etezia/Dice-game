using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemChecker : MonoBehaviour
{
    public Text GPUName;
	public Text GPUMemory;
	public Text GPUType;

	public Text DeviceName;
    public Text DeviceModel;
    public Text BatteryLevel;
	public Text DeviceType;

	public Text CPUCores;
	public Text CPUFrequency;
	public Text CPUType;

	public Text RAM;

	public Text RTSSupport;


	void Awake()
    {
		GPUName.text = Convert.ToString(SystemInfo.graphicsDeviceName);
		GPUMemory.text = Convert.ToString(SystemInfo.graphicsMemorySize);
		GPUType.text = Convert.ToString(SystemInfo.graphicsDeviceType);

		DeviceName.text = Convert.ToString(SystemInfo.deviceName);
		DeviceModel.text = Convert.ToString(SystemInfo.deviceModel);
		DeviceType.text = Convert.ToString(SystemInfo.deviceType);

		CPUCores.text = Convert.ToString(SystemInfo.processorCount);
		CPUFrequency.text = Convert.ToString(SystemInfo.processorFrequency);
		CPUType.text = Convert.ToString(SystemInfo.processorType);

		RAM.text = Convert.ToString(SystemInfo.systemMemorySize);

		RTSSupport.text = Convert.ToString(SystemInfo.supportsRayTracing);
	}

    void Update()
    {
        BatteryLevel.text = Convert.ToString(SystemInfo.batteryLevel * -100) + "%";
    }
}
