// JavaScript source code
import http from 'k6/http';
import { check, group, sleep } from 'k6';

export const options = {
    stages: [
        { target: 1, duration: '4m' },
        { target: 10, duration: '3m' },
        { target: 0, duration: '2m' },
    ]
};

export default function () {

    const res = http.get('https://localhost:7199/api/v1/Users');

    sleep(1);
    const checkRes = check(res, {
        'Success': (r) => r.status === 200
    })

};