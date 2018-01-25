# Automated-EmailSender
Complete code to send automatic mails using MongoDB,HTML file,Smtp

This is a console app. (a back-end service which runs on demand)

There are two collections in Mongo DB

Merchants:
Id
Name
PartnerId
EmailAddress
Phone

Partners:
Id
BusinessName
BannerImageUrl
Color (hex color code)

An email template is created to use it later.

There are 3 entries in the partners collection.Similarly, merchants collection has 6 entries which have PartnerId from the Ids of partner collection. (which means any two entries in the merchants collection have same partner id).
Console app written in C# reads all the merchant entries and generates a complete html for each merchant with all the data in the html template replaced accordingly. Generated html is sent as an email to the merchant's email address.
