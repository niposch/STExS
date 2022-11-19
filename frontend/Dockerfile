from node:latest as node
workdir /app
copy package.json /app
run yarn
copy . .
run yarn build --configuration=production

#state 2
FROM nginx:alpine
COPY --from=node /app/dist/stex-s /usr/share/nginx/html
COPY conf/ /etc/nginx


