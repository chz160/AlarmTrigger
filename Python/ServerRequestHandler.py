import http.server

class WebRequestHandler(http.server.SimpleHTTPRequestHandler):
    
	def do_GET(self):
		myOutput = ""
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
			myOutput = "{ message:'Alarm Is Up' }"
		else:
			self.send_response(200)
			self.send_header("Content-type", "application/json")
			
		self.send_header("Content-length", str(len(myOutput)))
		self.end_headers()
		self.wfile.write(bytes(myOutput))