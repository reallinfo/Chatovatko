# Chatovatko
A chatting C# application with end-to-end encryption. The main purpuse of application is to deliver solution for secure chatting, where you can be sure your messages will remain private even if server will be misused.
The application is now under development. The works on front end hasn't started yet.

Chatovatko is currently tested under Ubuntu 18.04 and Windows 10.

## Function overview
* End-to-end encryption
* Backuping encrypted conversations to server
* On live chatting (chat, where messages are not sended, but chat textbox is cloned to recepient)
* Multiplatform client app (Windows, Android, iOS)
* Multiplatform server app (Windows, Linux)

## Important technical details
* **.NET Core 2.1** used for server-side service and for command-based console application (for testing porpuse)
* **.NET Standart 2.0** used for multiplatform client libraries and server-client shared libraries
* **Tls 1.2** is used for encrypting communication between server and client and for server verification
* **RSA** with key size **4096** is used for client verification and sending AES keys
* **AES256** used for encrypting messages
* **MySql** is required as server-side database.
* Sqlite3 is used as client side database
* The project is developed using **Visual Studio 2017** and **MySql Workbench 8.0**
* Also, the project also benefits from Entity Framework Core, Newtonsoft.Json and Pomelo.EntityFrameworkCore.MySql
* SHA256 and SHA1 used for hashing
* Only little endian is supported

## Getting started
There is no offical release currently, but you can download git repository and compile.
To clone repository, enter to your console:
    
    git clone https://github.com/stastnypremysl/Chatovatko/
    
For deep technical details, please, visit wiki.

### Installation of server
Install `MySql 14` and run script [`Chatovatko/sql/serverBuildScripts/build.sql`](https://github.com/stastnypremysl/Chatovatko/blob/master/sql/serverBuildScripts/build.sql). It is necessary to have `.NET Core 2.1` installed. This [manual](https://www.microsoft.com/net/learn/get-started-with-dotnet-tutorial#install) seems to be useful.

You will need **p12 certificate**. You can run this script to generate it: [`Chatovatko/scripts/genarateCert.sh`](https://github.com/stastnypremysl/Chatovatko/blob/master/scripts/genarateCert.sh).

Then make a config file. Here is an example for inspiration:

    ConnectionString=Server=localhost; database=chatovatko;UID=MyUserName;password=SuperSecretPassword
    CertAddress=/mnt/c/keys/private.p12
    ServerName=Unforgattable server name

When's everything ready, run server with this command

    cd /pathToRepository/Chatovatko/Premy.Chatovatko/Premy.Chatovatko.Server
    dotnet run -c Release -- /otherPath/configFile.txt

Server uses **TCP** in ports **8470-8472**.
    
### Console client
#### Installation
As it was in server installation, install [`.NET Core 2.1`](https://www.microsoft.com/net/learn/get-started-with-dotnet-tutorial#install) if you haven't done it already. There are no more prerequsities. Just run

    cd /pathToRepository/Chatovatko/Premy.Chatovatko/Premy.Chatovatko.Client.Console
    dotnet run -c Release

#### First run
There are two inicialization commands:

    init new <server_address>

This one will generate new p12 certificate and you will be asked to enter path to save it. **It is necessary to keep it SAFE!** After that you will be asked to enter your new unique username.

    init login <server_address>
    
If you have your own p12 certificate, use this one. If the certificate haven't been paired with this server already, the entered username will be used for your registration. Otherwise the username will be ignored.

#### Reset/Relogin
If anything goes wrong, you can always relogin. Run

    delete database

and continue as first time.

#### Casual commands
To exit application, just enter

    exit
or

    quit
If you want add comments to your script, please respect this convention

    # <comment>
    -- <comment>

#### Online only commands
To open new connection to server, use

    connect
 
If a connection crashed, or you just wanted to disconnect, run
 
    disconnect
     
##### Pulling and pushing
Almost all changes are kept in local database. It is necessary to push them on server to finilize them. Analogily the same, you need pull if you want download all changes, which are on server already. Two magical selfdescribing keywords:

    push
    pull
    
##### Trutification
Before you can send messages to an user, you must trustify him. To do so, enter

    trust <user_id>

This will send server information, you trust this user and generate confirmatory message to user's chain. If you've done this first time, it will also create new AES key and send it encryped to server. The key is for encrypting your messages for the user and for his ability of reading it.

The user must have trustifed you to receive your messages.

If you change your mind and want to untrust some user, just enter to console

    untrust <user_id>
    
Please, remember the already loaded messages from untrusted users will not be loaded again after database deletion.

#### Offline commands
These commands will be always executed offline and it is necessary to pull/push to keep everything up-to-date.

##### Lists
To list all users, please enter

    ls users
     
To list all of your threads, please enter

    ls threads
   
Each thread has its own private id (`id`) and its public id (`public_id`). Private id is unique only on the client, but public id is unique globally.

Thread writeout can be invoced by

    ls messages <private_thread_id>
    
##### Posts
A new thread can be created by

    post thread <user_id> <name>

Please, keep in mind, a name can't contain double space.

To send new message, please enter

    post message <private_thread_id> <eof>
    ......................
    ....message content...
    ......................
    <eof>
    
Remember that `<eof>` cannot contain any space.

##### Rename
A thread can be renamed by

    rename thread <private_thread_id> <name>
    
The rule for `<name>` are same as you were creating new thread.

##### Delete
To delete message thread, please enter

    delete thread <private_thread_id>
     
Messages can be deleted by

    delete message <message_id> 
