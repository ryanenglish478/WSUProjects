import socket

s = socket.socket()
"C:\Projects\TestFile.txt"
port=12345

#connect to server on local pc
print('Connecting to server')
s.connect(('127.0.0.1', port))
print("Connected, to quit type 'quit'")
print("To send a file, type 'file' and then enter path to the file")
message = input(" -> ")
while message.lower().strip() != 'quit':
    if(message.lower().strip() == 'file'):
        filepath = input("Enter file path: ")
        with open(filepath, 'rb') as sendFile:
            while True:
                chunk = sendFile.read(1024)
                if not chunk:
                    s.send(b' END')
                    break
                s.send(b'FILE ' + chunk)
        sendFile.close()
    else:
        s.send(message.encode())

    data = s.recv(1024)
    if(data.decode().startswith('FILE ')):
        with open('receivedServerFile.txt', 'wb') as recvFile:
            while not data.decode().endswith(' END'):
                fileContent = data.decode()[5:]
                recvFile.write(fileContent.encode())
                data = s.recv(1024)
            fileContent = data.decode()[5:-4]
            recvFile.write(fileContent.encode())
            recvFile.close()
        print("File Received! File contents written into receivedServerFile.txt")
    else:      
        print('Received from server: ' + data.decode())

    message = input(" -> ")

s.close()
