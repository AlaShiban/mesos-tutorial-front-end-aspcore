# What this is
This is an example of a ASP.net Core front-end web service. It's used as part of the Building Services at Scale course.

# How to run the project
First build the docker image by running the following command:
```
docker -H localhost:2375 build -t your-docker-hub-account-name/mesos-tutorial-front-end-aspcore 
```
Then run the sample service:
```
docker -H localhost:2375 run -p 5000:5000 -it  your-docker-hub-account-name/mesos-tutorial-front-end-aspcore 
```

you should now be able to access http://localhost:5000
