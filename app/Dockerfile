FROM node:16-alpine3.11

WORKDIR .

ENV PATH /app/node_modules/.bin:$PATH

COPY package.json .
COPY package-lock.json .
RUN npm i

COPY . .

CMD ["npm", "start"]