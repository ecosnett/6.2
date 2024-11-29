import sqlite3 

conn = sqlite3.connect("sites.db")
cur = conn.cursor()
cur.execute("CREATE TABLE IF NOT EXISTS sites (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, url TEXT)")

#cur.execute("INSERT INTO sites VALUES (NULL, 'github', 'www.github.com' )")
#cur.execute("DELETE FROM sites WHERE name = 'github' ")
conn.commit()
conn.close()