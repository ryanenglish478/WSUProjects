Checkins
cur.execute("UPDATE business SET numCheckins = %s WHERE business_id = %s", (checkinCount, id))

NumReviews
cur.execute("SELECT business_id, COUNT(*) as num_reviews " \
                + "FROM review "\
                + "GROUP BY business_id")
cur.execute("UPDATE business SET review_count = %s WHERE business_id = %s", (review_count, business_id))

reviewrating
cur.execute("SELECT business_id, AVG(stars) as avg_rating " \
                + "FROM review "\
                + "GROUP BY business_id")
cur.execute("UPDATE business SET reviewrating = %s WHERE business_id = %s", (review_rating, business_id))

NumBusinessInCategory
cur.execute("SELECT categoryname, COUNT(*) as num_category " \
                + "FROM contains "\
                + "GROUP BY categoryname")
cur.execute("UPDATE category SET numofbusiness = %s WHERE categoryname = %s", (num_category, categoryname))

Functions ins parseAndInsert.py that I sused to update the values:

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
