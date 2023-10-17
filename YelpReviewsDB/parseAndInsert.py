#Ryan English
#11617228
import json
import psycopg2

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")

def cleanStr(s):
    return s.replace("(","").replace(")","").replace(",","").replace("'","")

def int2BoolStr (value):
    if value == 0:
        return 'False'
    else:
        return 'True'

def insert2BusinessTable():
    #reading the JSON file
    with open('.//yelp_business.JSON','r') as f:    #TODO: update path for the input file
        line = f.readline()
        count_line = 0

        try:
            conn = psycopg2.connect("dbname='milestone2db' user='postgres' host='localhost' password='optixmagseries'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)
            sql_str = "INSERT INTO business(business_id,name,address,state,city,zipcode,stars,reviewrating,numCheckins) " \
                      "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + cleanStr4SQL(data["name"]) + "','" + cleanStr4SQL(data["address"]) + "','" + \
                      cleanStr4SQL(data["state"]) + "','" + cleanStr4SQL(data["city"]) + "'," + cleanStr4SQL(data["postal_code"]) + ","  + str(data["stars"]) + "," + "0.0,0);"
            try:
                cur.execute(sql_str)
            except:
                print("Insert to businessTABLE failed!")
            conn.commit()
            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    f.close()

def insert2StateTables():
    #reading the JSON file
    with open('.//yelp_business.JSON','r') as f:    
        line = f.readline()
        count_line = 0

        try:
            conn = psycopg2.connect("dbname='milestone2db' user='postgres' host='localhost' password='optixmagseries'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)
            sql_str = "INSERT INTO State(StateName) VALUES ('" + cleanStr4SQL(data['state']) + "');"
            try:
                cur.execute(sql_str)
            except:
                print("Insert to StateTable failed!")
            conn.commit()
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()

    print(count_line)
    f.close()

def insert2CityTables():
    #reading the JSON file
    with open('.//yelp_business.JSON','r') as f:    
        line = f.readline()
        count_line = 0

        try:
            conn = psycopg2.connect("dbname='milestone2db' user='postgres' host='localhost' password='optixmagseries'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)
            sql_str = "INSERT INTO City(CityName, State) VALUES ('" + cleanStr4SQL(data['city']) + "','" + cleanStr4SQL(data['state']) + "');"
            try:
                cur.execute(sql_str)
                print("Inserted into city table!")
            except:
                print("")
            conn.commit()
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()

    print(count_line)
    f.close()


def insert2ZipTables():
    #reading the JSON file
    with open('.//yelp_business.JSON','r') as f:    
        line = f.readline()
        count_line = 0

        try:
            conn = psycopg2.connect("dbname='milestone2db' user='postgres' host='localhost' password='optixmagseries'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)
            sql_str = "INSERT INTO zipcode(ZipCode,NumOfBusiness,City,State,AvgCheckin) VALUES ('"\
                    + cleanStr4SQL(data['postal_code']) + "',0,'" + cleanStr4SQL(data["city"]) + "','" +  cleanStr4SQL(data["state"]) + "',0.0);"
            try:
                cur.execute(sql_str)
            except:
                print("Insert to ZipCodeTable failed!")
            conn.commit()
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()

def insert2Category():
    #reading the JSON file
    with open('.//yelp_business.JSON','r') as f:    
        line = f.readline()
        count_line = 0

        try:
            conn = psycopg2.connect("dbname='milestone2db' user='postgres' host='localhost' password='optixmagseries'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)
            for item in data['categories']:
                sql_str = "INSERT INTO Category(CategoryName,NumOfBusiness) VALUES ('"\
                    + item + "',0);"
            
            try:
                cur.execute(sql_str)
            except:
                print("Insert to Category failed!")
            conn.commit()
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()

def insert2Contains():
    #reading the JSON file
    with open('.//yelp_business.JSON','r') as f:  
        line = f.readline()
        count_line = 0

        try:
            conn = psycopg2.connect("dbname='milestone2db' user='postgres' host='localhost' password='optixmagseries'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)
            for item in data['categories']:
                sql_str = "INSERT INTO Contains(CategoryName,business_id) VALUES ('"\
                    + item + "','" + cleanStr4SQL(data['business_id']) + "');"
            
            try:
                cur.execute(sql_str)
            except:
                print("Insert to Contains failed!")
            conn.commit()
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()

def insert2User():
    #reading the JSON file
    with open('.//yelp_user.JSON','r') as f:    
        line = f.readline()
        count_line = 0
        try:
            conn = psycopg2.connect("dbname='milestone2db' user='postgres' host='localhost' password='optixmagseries'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)
            sql_str = "INSERT INTO \"User\"(UserName,user_id) VALUES ('"\
                    + cleanStr4SQL(data["name"]) + "','" + cleanStr4SQL(data["user_id"]) + "');"
            
            try:
                cur.execute(sql_str)
            except:
                print("Insert to User failed!")
            conn.commit()
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()

def insert2Review():
    #reading the JSON file
    with open('.//yelp_review.JSON','r') as f:    
        line = f.readline()
        count_line = 0
        try:
            conn = psycopg2.connect("dbname='milestone2db' user='postgres' host='localhost' password='optixmagseries'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()
        while line:
            data = json.loads(line)
            sql_str = "INSERT INTO review(Stars,user_id,business_id) VALUES ("\
                    + str(data['stars']) + ",'" + cleanStr4SQL(data["user_id"]) + "','" +  cleanStr4SQL(data["business_id"]) + "');"
            
            try:
                cur.execute(sql_str)
            except:
                print("Insert to Review failed!")
            conn.commit()
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()

def countCheckins(business_id):
    total_checkins = 0
    
    with open('.//yelp_checkin.JSON','r') as f:    
        line = f.readline()
        count_line = 0
        while line:
            data = json.loads(line)
            if data['business_id'] == business_id:
                for day, times in data['time'].items():
                    for time, count in times.items():
                        total_checkins+=count
                return total_checkins
            line = f.readline()
            count_line+=1


    

def updateCheckins():
    try:
        conn = psycopg2.connect("dbname='milestone2db' user='postgres' host='localhost' password='optixmagseries'")
    except:
        print('Unable to connect to the database!')
    cur = conn.cursor()

    cur.execute("SELECT business_id FROM business")
    business_ids = cur.fetchall()

    for id in business_ids:
        print(cleanStr(str(id)))
        checkinCount  = countCheckins(cleanStr(str(id)))
        print("Checkin: %s",(checkinCount))
        cur.execute("UPDATE business SET numCheckins = %s WHERE business_id = %s", (checkinCount, id))
        conn.commit()

    cur.close()
    conn.close()

def updateReviewCount():
    try:
        conn = psycopg2.connect("dbname='milestone2db' user='postgres' host='localhost' password='optixmagseries'")
    except:
        print('Unable to connect to the database!')
    cur = conn.cursor()

    cur.execute("SELECT business_id, COUNT(*) as num_reviews " \
                + "FROM review "\
                + "GROUP BY business_id")
    reviewCount = cur.fetchall()

    for item in reviewCount:
        business_id = item[0]
        review_count = item[1]
        cur.execute("UPDATE business SET review_count = %s WHERE business_id = %s", (review_count, business_id))
        conn.commit()

    cur.close()
    conn.close()

def updateRating():
    try:
        conn = psycopg2.connect("dbname='milestone2db' user='postgres' host='localhost' password='optixmagseries'")
    except:
        print('Unable to connect to the database!')
    cur = conn.cursor()

    cur.execute("SELECT business_id, AVG(stars) as avg_rating " \
                + "FROM review "\
                + "GROUP BY business_id")
    reviewCount = cur.fetchall()

    for item in reviewCount:
        business_id = item[0]
        review_rating = item[1]
        cur.execute("UPDATE business SET reviewrating = %s WHERE business_id = %s", (review_rating, business_id))
        conn.commit()

    cur.close()
    conn.close()

def updateCategoryNum():
    try:
        conn = psycopg2.connect("dbname='milestone2db' user='postgres' host='localhost' password='optixmagseries'")
    except:
        print('Unable to connect to the database!')
    cur = conn.cursor()

    cur.execute("SELECT categoryname, COUNT(*) as num_category " \
                + "FROM contains "\
                + "GROUP BY categoryname")
    reviewCount = cur.fetchall()

    for item in reviewCount:
        categoryname = item[0]
        num_category = item[1]
        cur.execute("UPDATE category SET numofbusiness = %s WHERE categoryname = %s", (num_category, categoryname))
        conn.commit()

    cur.close()
    conn.close()

def updateAvgCheckin():
    try:
        conn = psycopg2.connect("dbname='milestone2db' user='postgres' host='localhost' password='optixmagseries'")
    except:
        print('Unable to connect to the database!')
    cur = conn.cursor()

    cur.execute("SELECT zipcode, AVG(numcheckins) as avg_checkins " \
                + "FROM business "\
                + "GROUP BY zipcode")
    reviewCount = cur.fetchall()
    for item in reviewCount:
        zipcode = item[0]
        avg_checkins = item[1]
        poo = "UPDATE zipcode SET AvgCheckin = %s WHERE zipcode = '%s'", (avg_checkins, zipcode)
        cur.execute("UPDATE zipcode SET AvgCheckin = %s WHERE zipcode = '%s'", (avg_checkins, zipcode))
        conn.commit()

    cur.close()
    conn.close()



#insert2BusinessTable()
#insert2StateTables()
#insert2CityTables()
#insert2ZipTables()
#insert2Category()
#insert2Contains()
#insert2User()
#insert2Review()
#updateCheckins()
#updateReviewCount()
#updateRating()
#updateCategoryNum()
updateAvgCheckin()
print("DONE")