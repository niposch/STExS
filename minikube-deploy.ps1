#requires -PSEdition Core

echo "make sure you've followed the instructions here https://kubernetes.io/docs/tasks/access-application-cluster/ingress-minikube/"

minikube image load stexs-backend
minikube image load stexs-frontend

minikube kubectl -- apply -f deployment.yaml;
minikube kubectl -- apply -f localhost-ingress.yaml;


echo "Please run `minikube tunnel` and access stexs at http://stexs.localhost/"