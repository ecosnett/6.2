import pyodbc # type: ignore 
from datetime import datetime

dt = datetime.strptime('1111-01-01 01:01:01', '%Y-%m-%d %H:%M:%S')

server = 'dblogs.database.windows.net'  
database = 'logs'  
username = 'azureadmin' 
password = 'Tr1vial5'  
driver = '{ODBC Driver 17 for SQL Server}'  

conn_str = f'DRIVER={driver};SERVER={server};DATABASE={database};UID={username};PWD={password}'

try:
    conn = pyodbc.connect(conn_str)
    print("Connection successful!")

    cursor = conn.cursor()

    #cursor.execute("CREATE TABLE logs (timestamp DATETIME PRIMARY KEY, url VARCHAR(255),message VARCHAR(255));") 
    #cursor.execute("DELETE FROM logs")
    conn.commit()
 
    cursor.execute("SELECT * FROM logs")
    for i in cursor.fetchall():
        print(i)

    cursor.close()
    conn.close()
except Exception as e:
    print("Error:", e)
