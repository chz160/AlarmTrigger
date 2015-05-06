# import RPi.GPIO as GPIO
import time
import os
import http.server
from ServerRequestHandler import WebRequestHandler
#from WifiScanner import Scanner

# GPIO.setmode(GPIO.BOARD)
# GPIO.setup(3, GPIO.OUT)
# GPIO.output(3, True)
PORT = 8080
RequestHandler = WebRequestHandler
Server = http.server.HTTPServer(('',PORT), RequestHandler)
#Wifi = Scanner()


def StartServer():
	#access_control_list = Wifi.access_points_list
	#print("Access Points", Wifi.GetAccessPoints())
	Server.serve_forever()

def StopServer():
	Server.shutdown()

if __name__ == '__main__':
	#Server = HTTPServer(('',PORT), RequestHandler)

	print("Serving at port", PORT)
	try:
		print("Starting...")
		StartServer()
	except (KeyboardInterrupt, SystemExit):
		print("Finishing...")
	finally:		
		print("Stopping...")
		StopServer()
		#GPIO.output(3, False)
		#GPIO.cleanup()print('Hello World')
