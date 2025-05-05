import React from 'react'
import {FiTrendingDown, FiTrendingUp} from "react-icons/fi";

export const StatCards = () => {
    return <>
        <Card
            title="Bookings Today"
            value="5"
            pillText="25%"
            trend="up"
            period="Compared to yesterday"
        />
        <Card
            title="Users"
            value="176"
            pillText="10%"
            trend="down"
            period="From Jan 1st - Apr 30th"
        />
        <Card
            title="Rooms"
            value="20"
            pillText="5%"
            trend="up"
            period="Previous 365 days"
        />
        </>;

};

const Card = ({title, value, pillText, trend, period}: {
    title: string;
    value: string;
    pillText: string;
    trend: "up" | "down";
    period: string;
}) => {
    return <div className="col-span-4 p-4 rounded border border-[--color-text-grey]">
        <div className="flex mb-8 items-start justify-between">
            <div>
                <h3 className="text-stone-500 mb-2 text-sm">{title}</h3>
                <p className="text-3xl font-semibold text-white">{value}</p>
            </div>
            <span className={`text-xs flex items-center gap-1 font-medium px-2 py-1 rounded ${
                trend === "up"
                ? "bg-green-100 text-green-700"
                    : "bg-red-100 text-red-700"
            }`}
            >
                {trend === "up" ? <FiTrendingUp/> : <FiTrendingDown/>}
                {pillText}
            </span>
        </div>
        <p className="text-xs text-stone-500">{period}</p>
    </div>
}