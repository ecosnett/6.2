import sqlite3 

conn = sqlite3.connect("D:\\Users\\edward\\codes\\C#\\PingWebApp\\sites.db")

cur = conn.cursor()
cur.execute("CREATE TABLE IF NOT EXISTS sites (url TEXT PRIMARY KEY, name TEXT)")

cur.execute("INSERT INTO sites (url, name) VALUES ('www.github.com', 'github')")
#cur.execute("DELETE FROM sites WHERE name = 'github' ")
conn.commit()

cur.execute("CREATE TABLE IF NOT EXISTS site_logs (timestamp TEXT PRIMARY KEY, url TEXT, message TEXT, FOREIGN KEY (url) REFERENCES sites(url))")
conn.commit()
conn.close()