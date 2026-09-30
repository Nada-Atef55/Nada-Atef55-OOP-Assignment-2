
# AppointmentDesk 
responsibilities :
- check if appointment is within working hours 
- find next available slot
- book a slot
- create a calender event for patients
- send SMS reminder

problems :
the class shouldn't create calender event or send SMS,
it has to be responsible only for receptionist operations that belong to the slot

for example: if a change in calender or SMS format is needed , it will lead to a change in the class 




# CheckoutBasket
responsibilites :
- Manage order basket through 
  adding new line, apply coupon , gift wrapping and calculate price and discount

- generate customer message
- authorize payment

problems:
any change in customer message format , in payment or coupons ,leads to same class modification




# CourseEnrollmentDesk
responsibilities:
- manage course enrollment through 
  register students or add them to waiting list
- generate welcome message to the student
- generate message with course price detatils
- promote students from waiting list 

problems:
any change in welcome message, promotion policy or course pricing, leads to change the same class



# GradeBook
responsibilties:
- create academic record for students 
- calculate grade using letters
- determine whether the student meets honor roll
- generate transcription that contains student record details
- export all student records as CSV file

problems:
any change in grading policy or transcript format, leads to change same class




# KitchenTicket
responsibilties:
- add food item
- detect allergens 
- organize kitchen through detecting minutes for the food to be ready and
  the lane that has to prepare food
- print kitchen ticket depending on printer features

problems:
any change in allergens or printing format and printer vendor, leads to change same class




# LoanDesk
responsibilities:
- calculate risk score, detect eligibility
- generate decision letter to the applicant
- export applicant data as CSV

problems:
any change in risk score policy, decision letter formatting or data to be exported , leads to change same class




# SubscriptionBilling
responsibilities:
- calculate prorate subscription price
- generate next invoice number
- create reminder for payment
- export data

problems:
any change in invoice number format ,reminder email format or data format  , leads to change same class



# SupportTicket
responsibilities:
- manage support ticket ceation
- calculate ticket priority depending on text
- set deadline to respond to a ticket
- create a reply message to the customer ticket
- generate internal message with ticket info

problems:
the class can change by chaging  ticket priority calculation policy, response deadline 
or reply message format




# WardBoard
responsibilities:
- assign bed to patient
- calculate the patient acuity score
- create nurse handoff note with patient info
- Retrieve all pager log messages and clear log
- export bed, patient and acuity data


problems:
the class can change by chaging nurse note format or exported data format




# WarehousePickList
responsibilities:
- get a product from warehouse when needed
- specify the picked quantity of the product
- decide walking order of warehouse picker
- generate message that has picking info for the picker and shortage warning
- convert picking info into XML format that can be sent to WMS


problems:
the class can change by chaging walking order, picker message format or exported data format


