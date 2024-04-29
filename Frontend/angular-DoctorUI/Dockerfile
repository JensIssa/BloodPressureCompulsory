FROM node:20.12.2 as build
WORKDIR /app
COPY /Frontend/angular-DoctorUI/package.json ./package.json
RUN npm install
RUN npm install -g @angular/cli
COPY /Frontend/angular-DoctorUI/ ./
RUN ng build
FROM nginx:alpine
COPY --from=build /app/dist/angular-doctor-ui/browser /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
