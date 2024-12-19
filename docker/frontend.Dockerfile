FROM node:18-alpine

WORKDIR /app

COPY src/whaletracker_frontend/package*.json ./

RUN npm install

COPY src/whaletracker_frontend/ .

EXPOSE 3000

CMD ["npm", "start"] 