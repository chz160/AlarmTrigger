from wifi import Cell, Scheme

class Scanner:
	access_points_list = []
	
	def __init__ (self):
		self.access_points_list = Cell.all('wlan0')		