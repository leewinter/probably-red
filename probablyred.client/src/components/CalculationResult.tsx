const CalculationResult = (props: { calculationResult: any }) => {
  const { calculationResult } = props;

  return (
    <div>
      {calculationResult ? (
        <div>
          <table>
            <tbody>
              <tr>
                <th>Strategy</th>
                <td>{calculationResult.calculationStrategy}</td>
              </tr>
              <tr>
                <th>Success</th>
                <td>
                  {calculationResult.success ? (
                    <span className="success">&#10004;</span>
                  ) : (
                    <span className="error">&#x2717;</span>
                  )}
                </td>
              </tr>
              <tr>
                <th>Result</th>
                <td>{calculationResult.result}</td>
              </tr>
              {calculationResult.validationErrors.length
                ? calculationResult.validationErrors.map(({ errorMessage }: any, index: number) => {
                    return (
                      <tr key={index} className="error">
                        <th>{index === 0 ? 'Errors' : null}</th>
                        <td>{errorMessage}</td>
                      </tr>
                    );
                  })
                : null}
            </tbody>
          </table>
        </div>
      ) : null}
    </div>
  );
};

export default CalculationResult;
