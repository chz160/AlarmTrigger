# import RPi.GPIO as GPIO
import time
import os
import http.server
from ServerRequestHandler import WebRequestHandler

# GPIO.setmode(GPIO.BOARD)
# GPIO.setup(3, GPIO.OUT)
# GPIO.output(3, True)
PORT = 8080
RequestHandler = WebRequestHandler
Server = http.server.HTTPServer(('',PORT), RequestHandler)

def StartServer():
	Server.serve_forever()

def StopServer():
	Server.shutdown()

if __name__ == '__main__':
	#Server = HTTPServer(('',PORT), RequestHandler)

	print("Serving at port", PORT)
	print("Starting...")
	try:
		#httpd.serve_forever()
		StartServer()
	except (KeyboardInterrupt, SystemExit):
		print("Finishing...")
	finally:
		StopServer()
		#GPIO.output(3, False)
		#GPIO.cleanup()print('Hello World')
