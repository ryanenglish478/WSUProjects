import socket
import sys

s = socket.socket()
print("Socket successfully created")
#Reserve port on computer
port = 12345

#bind port to socket without ip address
#So server listens to requests
s.bind(('', port))

#put socket into listening mode
s.listen(5)
print("socket is listening")

#establish connection with client
conn, addr = s.accept()
print('Got connection from', addr)
print("To send a file, type 'file' and then enter path to the file")

while True:
  #Receive the data as bytes
  data = conn.recv(1024)
  if not data:
    break
  
  #Check header to see if file
  if(data.decode().startswith('FILE ')):
    with open('receivedClientFile.txt', 'wb') as recvFile:
      #While file does not get to END, write file contents
      while not data.decode().endswith(' END'):
        fileContent = data.decode()[5:]
        recvFile.write(fileContent.encode())
        data = conn.recv(1024)
      
      #Writes the data without the FILE and END headers
      endData = data.decode()[5:-4]
      recvFile.write(endData.encode())
      recvFile.close()
      print("File Received! File contents written into receivedClientFile.txt")
  else:
    print('Received from client: ' + data.decode())
  
  message = input(" -> ")
  if(message.lower().strip() == 'file'):
    filepath = input("Enter file path: ")
    with open(filepath, 'rb') as sendFile:
       while True:
        chunk = sendFile.read(1024)
        if not chunk:
          conn.send(b' END')
          break
        conn.send(b'FILE ' + chunk)
    sendFile.close()
  else:
    conn.send(message.encode())




conn.close()

