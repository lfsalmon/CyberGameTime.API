import sys
import tinytuya
import json

def turn_on(dev_id, ip, local_key):
    device = tinytuya.Device(dev_id, ip, local_key)
    device.set_version(3.4)
    result = device.set_status(True, 1)
    return result

def turn_off(dev_id, ip, local_key):
    device = tinytuya.Device(dev_id, ip, local_key)
    device.set_version(3.4)
    result = device.set_status(False, 1)
    return result

def get_status(dev_id, ip, local_key):
    device = tinytuya.Device(dev_id, ip, local_key)
    device.set_version(3.4)
    data = device.status()
    return data

def get_info(dev_id, ip, local_key):
    device = tinytuya.Device(dev_id, ip, local_key)
    device.set_version(3.4)
    data = device.product()
    return data

if __name__ == "__main__":
    if len(sys.argv) < 5:
        print("Usage: tinytuya_device.py <action> <dev_id> <ip> <local_key>")
        sys.exit(1)
    action = sys.argv[1]
    dev_id = sys.argv[2]
    ip = sys.argv[3]
    local_key = sys.argv[4]

    if action == "on":
        result = turn_on(dev_id, ip, local_key)
    elif action == "off":
        result = turn_off(dev_id, ip, local_key)
    elif action == "status":
        result = get_status(dev_id, ip, local_key)
    elif action == "info":
        result = get_status(dev_id, ip, local_key)
    else:
        print("Unknown action")
        sys.exit(1)

    print(json.dumps(result))