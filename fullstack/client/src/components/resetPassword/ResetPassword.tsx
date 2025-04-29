import React, {useState} from "react";
import "../styles.css";
import {AiFillEyeInvisible, AiFillEye} from "react-icons/ai";

const ResetPassword = () => {
    const [showPassword, setShowPassword] = useState(false);
    const [showConfirmPassword, setShowConfirmPassword] = useState(false);

    return (
        <div className="bg-background-black flex items-center justify-center min-h-screen">
            <div className="flex items-center justify-center min-h-screen">
                <div className="bg-background-grey border border-white/10 rounded-2xl p-10 w-full max-w-lg">
                    <h2 className="text-center text-white text-xl mb-6">Reset Password</h2>
                    <p className="text-center text-text-grey mb-4">Please enter your new password</p>

                    <form className="space-y-6">
                        {/* New Password Field */}
                        <div className="relative">
                    <input
                        type={showPassword ? "text" : "password"}
                        placeholder="New Password"
                        className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                    />
                            <div
                                className="absolute top-4 right-4 cursor-pointer"
                                onClick={() => setShowPassword(!showPassword)}
                            >
                                {showPassword ? <AiFillEyeInvisible color="white" /> : <AiFillEye color="white" />}
                            </div>
                        </div>

                        {/* Confirm Password Field */}
                        <div className="relative">
                    <input
                        type={showConfirmPassword ? "text" : "password"}
                        placeholder="Confirm New Password"
                        className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                    />
                            <div
                                className="absolute top-4 right-4 cursor-pointer"
                                onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                            >
                                {showConfirmPassword ? <AiFillEyeInvisible color="white" /> : <AiFillEye color="white" />}
                            </div>
                        </div>


                    <button className="w-full py-3 rounded-md text-[var(--color-background-black)] bg-[var(--color-button-grey)] hover:bg-blue-500 hover:text-white">
                        Reset Password
                    </button>
                    </form>
                </div>
            </div>
        </div>
    );
}

export default ResetPassword;