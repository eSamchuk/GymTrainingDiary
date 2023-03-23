// JavaScript source code
import http from 'k6/http';
import { check, group, sleep } from 'k6';

export const options = {
    stages: [
        { target: 400, duration: '1m' },
        { target: 0, duration: '1m' }
    ]
};

export default function() {

    const res = http.get('https://localhost:7199/api/v1/Workouts');

    sleep(1);
    const checkRes = check(res, {
        'Success': (r) => r.status === 200
    })

};