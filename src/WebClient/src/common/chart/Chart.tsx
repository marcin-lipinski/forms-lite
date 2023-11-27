import { PartakeSessionFinishResponse, PartakeSessionSingleResult } from "../../models/session";
import './Chart.css';

interface Props {
    result: PartakeSessionFinishResponse
}

export default function Chart({result}: Props) {
    const colors = ["#565264", "#706677", "#A6808C", "#CCB7AE"];

    const compareUserAnswer = (question: PartakeSessionSingleResult) => {
        let value = question.answers.find(answer => answer.text === question.participantAnswer)?.value;
        return `Your answer was \"${question.participantAnswer}\". You answered the same as ${value! * 100}% of participants.`
    }

    const dyeChartCircle = (question: PartakeSessionSingleResult) => {
        let val1= question.answers[0].value * 100;
        let val2 = question.answers[1].value * 100;
        let val3 = question.answers[2].value * 100;
        let val4 = question.answers[3].value * 100;

        let result = `conic-gradient(`;
        result = result + `${colors[0]} 0 ${val1}%,`;
        result = result + `${colors[1]} 0 ${val1 + val2}%,`;
        result = result + `${colors[2]} 0 ${val1 + val2 + val3}%,`;
        result = result + `${colors[3]} 0 ${val1 + val2 + val3 + val4}%)`;
        console.log(result)
        return result;
    }

    return (
        <div className="chart">
            <p>{result.quizTitle}</p>
            {result.results.map((question, index) =>
                <div className="chart-question" key={index}>
                    <div>{question.questionContent}</div>
                    <div className="chart-question-circle" style={{background: dyeChartCircle(question)}}></div>
                    <div className="chart-question-keys">
                        {question.answers.map((answer, index2) => 
                            <div className="key" key={index2}>
                                <span className={`key-value hex${colors[index2].substring(1)}`}>{answer.value * 100}%</span>
                                <div className={`key-name`}>{answer.text}</div>
                            </div>
                        )}
                    </div>
                    <div className="chart-question-user">
                        {compareUserAnswer(question)}
                    </div>
                    <hr style={{width: "100%"}}></hr>
                </div>
            )}
        </div>
    )
}