import http.server
from WifiScanner import Scanner

class WebRequestHandler(http.server.SimpleHTTPRequestHandler):

	def do_GET(self):
		serverOutput = ""
		print(self.path)
		if self.path == "/":
			print("Index.html")
			self.send_response(200)
			self.send_header("Content-type", "text/html")
			self.path = "/Views/Index.html"
			http.server.SimpleHTTPRequestHandler.do_GET(self)
		elif self.path == "/Content/Site.css":
			self.send_response(200)
			self.send_header("Content-type", "text/css")
			self.path = "/Views/Content/Site.css"
			http.server.SimpleHTTPRequestHandler.do_GET(self)
		elif self.path == "/on":
			self.send_response(200)
			self.send_header("Content-type", "application/json")
			#GPIO.output(3, False)
		elif self.path == "/off":
			self.send_response(200)
			self.send_header("Content-type", "application/json")
			#GPIO.output(3, True)
		elif self.path == "/status":
			self.send_response(200)
			self.send_header("Content-type", "application/json")
			serverOutput = "{ message:'Alarm Is Up' }"
		elif self.path == "/wifi/list":
			access_points = Scanner().access_points_list
			json = "{\"access_points\" : ["
			for index, cell in enumerate(access_points):
				stripped = cell.ssid.strip("\x00")
				if stripped:
					json += "{\"ssid\" : \"" + cell.ssid + "\", \"address\" : \"" + cell.address + "\"},"
			if json.endswith(","):
				json = json[:-1]			
			json += "]}"
			self.send_response(200)
			serverOutput = json
		else:
			#This needs to be changed
			self.send_response(200)
			self.send_header("Content-type", "application/json")
			
		self.send_header("Content-length", str(len(serverOutput)))
		self.end_headers()
		self.wfile.write(bytes(serverOutput, 'utf-8'))